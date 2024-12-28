#nullable enable
using UnityEngine;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationTimeCounter:ITickable
    {
        public float CurrentTimer { get; set; }
        bool _isActiveTimer;
        
        public void ResetTime()
        {
            CurrentTimer = 0;
        }
        
        public void Tick()
        {
            if (_isActiveTimer)
            {
                CurrentTimer += Time.deltaTime;
            }
        }

        public void RestartCount()
        {
            _isActiveTimer = true;
        }

        public void StopCount()
        {
            _isActiveTimer = false;
        }
    }
}