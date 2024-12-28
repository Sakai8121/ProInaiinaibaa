#nullable enable
using CodeRedCat._4kVectorLandscape.Demo.Scripts;
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
        GameStartFlagHolder _gameStartFlagHolder = null!;
        GameTimeHolder _gameTimeHolder = null!;
        EvaluationTextViewMono _evaluationTextViewMono = null!;
        CameraScroll _cameraScroll = null!;
        
        [Inject]
        public void Construct(HandStateHolder handStateHolder,HiddenObjectStateHolder hiddenObjectStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,EvaluationDecider evaluationDecider,
            EvaluationTimeCounter evaluationTimeCounter,EvaluationTargetTimeHolder evaluationTargetTimeHolder,
            GameScoreHolder gameScoreHolder,GameStartFlagHolder gameStartFlagHolder,
            GameTimeHolder gameTimeHolder,EvaluationTextViewMono evaluationTextViewMono,
            CameraScroll cameraScroll)
        {
            _handStateHolder = handStateHolder;
            _hiddenObjectStateHolder = hiddenObjectStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            _evaluationDecider = evaluationDecider;
            _evaluationTimeCounter = evaluationTimeCounter;
            _evaluationTargetTimeHolder = evaluationTargetTimeHolder;
            _gameScoreHolder = gameScoreHolder;
            _gameStartFlagHolder = gameStartFlagHolder;
            _gameTimeHolder = gameTimeHolder;
            _evaluationTextViewMono = evaluationTextViewMono;
            _cameraScroll = cameraScroll;
        }
        
        void Update()
        {
            CheckPlayerInput();
        }

        void CheckPlayerInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                if (!_gameStartFlagHolder.IsStartingGame) return;
                
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Close);
                _evaluationTimeCounter.RestartCount();
            }
            
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return))
            {
                if (!_gameStartFlagHolder.IsStartingGame)
                {
                    _gameStartFlagHolder.StartGame();
                    _gameTimeHolder.RestartTimer();
                    _cameraScroll.ReStartCameraMove();
                    return;
                }
                
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
                enemy.EnemyViewMono.ChangeSpriteByEvaluationResult(evaluation);
                _gameScoreHolder.AddScore(evaluation);
                _evaluationTextViewMono.ActiveEvaluationText(evaluation);
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