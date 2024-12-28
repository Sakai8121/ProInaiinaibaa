#nullable enable
using Model.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class GameTimeHolder:ITickable
    {
        public readonly int GameLimitTime = 60;
        public float CurrentTime { get; set; }
        bool _isActiveTimer;

        TimeStateHolder _timeStateHolder;
        GameStartFlagHolder _gameStartFlagHolder;
        ResultViewMono _resultViewMono;
        GameScoreHolder _gameScoreHolder;
        
        [Inject]
        public GameTimeHolder(TimeStateHolder timeStateHolder,GameStartFlagHolder gameStartFlagHolder,
            ResultViewMono resultViewMono,GameScoreHolder gameScoreHolder)
        {
            _timeStateHolder = timeStateHolder;
            _gameStartFlagHolder = gameStartFlagHolder;
            _resultViewMono = resultViewMono;
            _gameScoreHolder = gameScoreHolder;
        }
        
        public void Tick()
        {
            if (_isActiveTimer)
            {
                CurrentTime += Time.deltaTime;
                CheckCurrentTimeState();
                CheckGameEnd();
                
                if(Input.GetKeyDown(KeyCode.Escape))
                    ExecuteEndGame();
            }
        }

        public void RestartTimer()
        {
            _isActiveTimer = true;
        }

        void StopTimer()
        {
            _isActiveTimer = false;
        }

        void CheckGameEnd()
        {
            if (CurrentTime >= GameLimitTime)
            {
                SoundManager.Instance.PlaySEOneShot(SESoundData.SE.EndClap);
                ExecuteEndGame();
            }
        }

        void ExecuteEndGame()
        {
            CurrentTime = GameLimitTime;
            StopTimer();
            SoundManager.Instance.StopBGM(BGMSoundData.BGM.PlayBgm);
            SoundManager.Instance.StopBGM(BGMSoundData.BGM.ZoneBgm);
            _gameStartFlagHolder.EndGame();
                
            _resultViewMono.ActiveResultUI(_gameScoreHolder.GameScore,_gameScoreHolder.EvaluationCount);
        }

        void CheckCurrentTimeState()
        {
            var currentTimeRate = CurrentTime / GameLimitTime;
            if (currentTimeRate > 0.66f)
            {
                _timeStateHolder.ChangeTimeState(TimeState.Night);
            }
            else if (currentTimeRate > 0.33f)
            {
                _timeStateHolder.ChangeTimeState(TimeState.Morning);
            }
            else
            {
                _timeStateHolder.ChangeTimeState(TimeState.Day);
            }
        }
    }
}