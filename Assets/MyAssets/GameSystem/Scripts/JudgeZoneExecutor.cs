#nullable enable
using System.Collections;
using System.Collections.Generic;
using Model.GameSystem;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class JudgeZoneExecutor : IInitializable
    {
        const int RequiredEvaluationCountOnZone = 3;
        
        [Inject]
        public JudgeZoneExecutor(GameScoreHolder gameScoreHolder,ZoneStateHolder zoneStateHolder)
        {
            gameScoreHolder.ObserveEveryValueChanged(holder => holder.EvaluationList)
                .Where(_ => !zoneStateHolder.IsZoneState())
                .Select(list => CheckCanEnterZoneMode(list)) // 評価だけを行う
                .Subscribe(result =>
                {
                    var (canEnterZone, updatedEvaluationList) = result;

                    if (canEnterZone)
                        zoneStateHolder.GoZoneMode();

                    // 必要に応じて EvaluationList を更新
                    gameScoreHolder.EvaluationList = updatedEvaluationList;
                });
        }

        (bool,List<EvaluationData.Evaluation>) CheckCanEnterZoneMode(List<EvaluationData.Evaluation> list)
        {
            List<EvaluationData.Evaluation> goodEvaluationList = new ();
            
            foreach (var evaluation in list)
            {
                if (evaluation == EvaluationData.Evaluation.Excellent)
                {
                    //エクセレント評価なら一回でゾーン状態に行ける
                    return (true,new List<EvaluationData.Evaluation>());
                }
                else if (evaluation == EvaluationData.Evaluation.Miss || evaluation == EvaluationData.Evaluation.Normal)
                {
                    //ミスやノーマル評価だとそれまで蓄積されていたGood評価が消える
                    return (false,new List<EvaluationData.Evaluation>());
                }
                else if(evaluation == EvaluationData.Evaluation.Good)
                {
                    //グッド評価だと蓄積される
                    goodEvaluationList.Add(evaluation);
                    if(goodEvaluationList.Count == RequiredEvaluationCountOnZone)
                        return (true,goodEvaluationList);
                }
            }
            
            return (false,goodEvaluationList);
        }
        
        //エントリーポイント用
        public void Initialize(){}
    }
}