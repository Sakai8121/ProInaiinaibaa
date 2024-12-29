#nullable enable
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationTargetTimeHolder
    {
        private float _currentTargetTime;

        public float CurrentTargetTime
        {
            get { return _currentTargetTime == 0 ? 3f : _currentTargetTime; }
            private set { _currentTargetTime = value; }
        }

        public void ChangeCurrentTargetTimeRandomly()
        {
            List<float> targetTimeList = new() { 1f, 2, 3 };
            _currentTargetTime = targetTimeList[Random.Range(0, targetTimeList.Count)];
        }
    }
}