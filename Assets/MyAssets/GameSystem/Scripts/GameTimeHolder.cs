#nullable enable
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class GameTimeHolder:ITickable
    {
        public readonly float GameLimitTime = 60;
        float CurrentTime { get; set; }
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

        void CheckCurrentTimeState()
        {
            var currentTimeRate = CurrentTime / GameLimitTime;
            if (currentTimeRate > 0.33f)
            {
                _timeStateHolder.ChangeTimeState(TimeState.Morning);
            }
            else if (currentTimeRate > 0.66f)
            {
                _timeStateHolder.ChangeTimeState(TimeState.Night);
            }
            else
            {
                _timeStateHolder.ChangeTimeState(TimeState.Day);
            }
        }
    }
}