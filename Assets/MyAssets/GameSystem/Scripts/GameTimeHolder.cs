#nullable enable
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

        [Inject]
        public GameTimeHolder(TimeStateHolder timeStateHolder)
        {
            _timeStateHolder = timeStateHolder;
        }
        
        public void Tick()
        {
            if (_isActiveTimer)
            {
                CurrentTime += Time.deltaTime;
                CheckCurrentTimeState();
                CheckGameEnd();
            }
        }

        public void RestartTimer()
        {
            _isActiveTimer = true;
        }

        public void StopTimer()
        {
            _isActiveTimer = false;
        }

        void CheckGameEnd()
        {
            if (CurrentTime >= GameLimitTime)
            {
                CurrentTime = GameLimitTime;
                StopTimer();
                SoundManager.Instance.PlaySEOneShot(SESoundData.SE.EndClap);
            }
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