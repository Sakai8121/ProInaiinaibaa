#nullable enable
using CodeRedCat._4kVectorLandscape.Demo.Scripts;
using Model.Enemy;
using Model.GameSystem;
using Model.Player;
using MyAssets.Enemy.Scripts;
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
        PlayerStateHolder _playerStateHolder;
        
        [Inject]
        public void Construct(HandStateHolder handStateHolder,HiddenObjectStateHolder hiddenObjectStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,EvaluationDecider evaluationDecider,
            EvaluationTimeCounter evaluationTimeCounter,EvaluationTargetTimeHolder evaluationTargetTimeHolder,
            GameScoreHolder gameScoreHolder,GameStartFlagHolder gameStartFlagHolder,
            GameTimeHolder gameTimeHolder,EvaluationTextViewMono evaluationTextViewMono,
            CameraScroll cameraScroll,PlayerStateHolder playerStateHolder)
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
            _playerStateHolder = playerStateHolder;
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
                
                //音
                switch (_playerStateHolder.CurrentPlayerState)
                {
                    case PlayerStateHolder.PlayerState.Default:
                        SoundManager.Instance.PlaySEOneShot(SESoundData.SE.CloseHandDefault);
                        break;
                    case PlayerStateHolder.PlayerState.God:
                        SoundManager.Instance.PlaySEOneShot(SESoundData.SE.CloseHandInZone);
                        break;
                }
            }
            
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return))
            {
                if (!_gameStartFlagHolder.IsStartingGame)
                {
                    _gameStartFlagHolder.StartGame();
                    _gameTimeHolder.RestartTimer();
                    _cameraScroll.ReStartCameraMove();
                    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.PlayBgm);
                    return;
                }
                
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Open);
                _evaluationTimeCounter.StopCount();
                
                //タイミングの評価を行った後、バトルする敵を変える。
                DecideEvaluation();
                ChangeBattleEnemy();
                
                //音
                switch (_playerStateHolder.CurrentPlayerState)
                {
                    case PlayerStateHolder.PlayerState.Default:
                        SoundManager.Instance.PlaySEOneShot(SESoundData.SE.OpenHandDefault);
                        break;
                    case PlayerStateHolder.PlayerState.God:
                        SoundManager.Instance.PlaySEOneShot(SESoundData.SE.OpenHandInZone);
                        break;
                }
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
                EvaluationSound(evaluation);
            });
        }

        void ChangeBattleEnemy()
        {
            _evaluationTargetTimeHolder.ChangeCurrentTargetTimeRandomly();
            _battleEnemySwitcher.SwitchToNextEnemy();
            _evaluationTimeCounter.ResetTime();
        }

        void EvaluationSound(EvaluationData.Evaluation evaluation)
        {
            switch (evaluation)
            {
                case EvaluationData.Evaluation.Excellent:
                    _battleEnemySwitcher.CurrentBattleEnemy.Do(enemy =>
                    {
                        if(enemy.EnemyKind == EnemyKind.Adult)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.AdultExcellent);
                        else  if(enemy.EnemyKind == EnemyKind.Baby)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.BabyExcellent);
                    });
                    break;
                case EvaluationData.Evaluation.Good:
                    _battleEnemySwitcher.CurrentBattleEnemy.Do(enemy =>
                    {
                        if(enemy.EnemyKind == EnemyKind.Adult)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.AdultGood);
                        else  if(enemy.EnemyKind == EnemyKind.Baby)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.BabyGood);
                    });
                    break;
                case EvaluationData.Evaluation.Miss:
                    _battleEnemySwitcher.CurrentBattleEnemy.Do(enemy =>
                    {
                        if(enemy.EnemyKind == EnemyKind.Adult)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.AdultMiss);
                        else  if(enemy.EnemyKind == EnemyKind.Baby)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.BabyMiss);
                    });
                    break;
                case EvaluationData.Evaluation.Normal:
                    _battleEnemySwitcher.CurrentBattleEnemy.Do(enemy =>
                    {
                        if(enemy.EnemyKind == EnemyKind.Adult)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.AdultBad);
                        else  if(enemy.EnemyKind == EnemyKind.Baby)
                            SoundManager.Instance.PlaySEOneShot(SESoundData.SE.BabyBad);
                    });
                    break;
            }
        }
    }
}