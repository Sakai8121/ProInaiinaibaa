#nullable enable
using Model.Enemy;
using Model.Player;
using UnityEngine;
using VContainer;

namespace MyAssets.GameSystem.Scripts
{
    public class PlayerInputMono:MonoBehaviour
    {
        HandStateHolder _handStateHolder = null!;
        HiddenObjectStateHolder _hiddenObjectStateHolder = null!;
        BattleEnemySwitcher _battleEnemySwitcher = null!;
        EvaluationDecider _evaluationDecider = null!;
        EvaluationTimeCounter _evaluationTimeCounter = null!;
        EvaluationTargetTimeHolder _evaluationTargetTimeHolder = null!;
        GameScoreHolder _gameScoreHolder = null!;
        
        [Inject]
        public void Construct(HandStateHolder handStateHolder,HiddenObjectStateHolder hiddenObjectStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,EvaluationDecider evaluationDecider,
            EvaluationTimeCounter evaluationTimeCounter,EvaluationTargetTimeHolder evaluationTargetTimeHolder,
            GameScoreHolder gameScoreHolder)
        {
            _handStateHolder = handStateHolder;
            _hiddenObjectStateHolder = hiddenObjectStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            _evaluationDecider = evaluationDecider;
            _evaluationTimeCounter = evaluationTimeCounter;
            _evaluationTargetTimeHolder = evaluationTargetTimeHolder;
            _gameScoreHolder = gameScoreHolder;
        }
        
        void Update()
        {
            CheckPlayerInput();
        }

        void CheckPlayerInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Close);
                _evaluationTimeCounter.RestartCount();
            }
            
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return))
            {
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Open);
                _evaluationTimeCounter.StopCount();
                
                //タイミングの評価を行った後、バトルする敵を変える。
                DecideEvaluation();
                ChangeBattleEnemy();
            }
            
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
            {
                _hiddenObjectStateHolder.SwitchHiddenObject();
            }
        }

        void DecideEvaluation()
        {
            var evaluation = _evaluationDecider.DecideEvaluation();
            _battleEnemySwitcher.CurrentBattleEnemy.Do(enemy =>
            {
                Debug.LogError(evaluation);
                enemy.EnemyViewMono.ChangeSpriteByEvaluationResult(evaluation);
                _gameScoreHolder.AddScore(evaluation);
            });
        }

        void ChangeBattleEnemy()
        {
            _evaluationTargetTimeHolder.ChangeCurrentTargetTimeRandomly();
            _battleEnemySwitcher.SwitchToNextEnemy();
            _evaluationTimeCounter.ResetTime();
        }
    }
}