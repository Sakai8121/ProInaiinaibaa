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
        ZoneStateHolder _zoneStateHolder;
        const int RequiredEvaluationCountOnZone = 3;
        
        [Inject]
        public JudgeZoneExecutor(GameScoreHolder gameScoreHolder,ZoneStateHolder zoneStateHolder)
        {
            _zoneStateHolder = zoneStateHolder;
            
            gameScoreHolder.ObserveEveryValueChanged(holder => holder.EvaluationList)
                //.Where(_ => !zoneStateHolder.IsZoneState())
                .Subscribe(list =>
                {
                    var (canEnterZone, updatedEvaluationList) = CheckCanEnterZoneMode(list);

                    if (canEnterZone)
                        zoneStateHolder.GoZoneMode();

                    // 必要に応じて EvaluationList を更新
                    gameScoreHolder.EvaluationList = updatedEvaluationList;
                });
        }

        public (bool,List<EvaluationData.Evaluation>) CheckCanEnterZoneMode(List<EvaluationData.Evaluation> list)
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
                    //ミスやノーマル評価だとそれまで蓄積されていたGood評価が消え、ゾーン状態ならゾーン状態を終了する
                    _zoneStateHolder.EndZoneMode();
                    return (false,new List<EvaluationData.Evaluation>());
                }
                else if(evaluation == EvaluationData.Evaluation.Good)
                {
                    //グッド評価だと蓄積される
                    goodEvaluationList.Add(evaluation);
                    if(goodEvaluationList.Count == RequiredEvaluationCountOnZone)
                        return (true,new List<EvaluationData.Evaluation>());
                }
            }
            
            return (false,goodEvaluationList);
        }
        
        //エントリーポイント用
        public void Initialize(){}
    }
}