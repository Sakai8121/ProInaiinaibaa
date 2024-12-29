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
            get { return _currentTargetTime == 0 ? 2f : _currentTargetTime; }
            private set { _currentTargetTime = value; }
        }

        public void ChangeCurrentTargetTimeRandomly()
        {
            List<float> targetTimeList = new() { 1.5f, 2, 2.5f };
            _currentTargetTime = targetTimeList[Random.Range(0, targetTimeList.Count)];
        }
    }
}