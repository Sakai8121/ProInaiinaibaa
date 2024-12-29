#nullable enable
using Model.GameSystem;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class ComboCountHolder
    {
        public int MaxComboCount => _maxComboCount;
        int _maxComboCount;
        public int CurrentComboCount { get; set; }

        public void TryIncrementComboCount(EvaluationData.Evaluation evaluation)
        {
            if (evaluation == EvaluationData.Evaluation.Miss || evaluation == EvaluationData.Evaluation.Normal)
                EndCombo();
            else
                IncrementComboCount();
        }

        public int GetScoreRate()
        {
            return 1 + Mathf.FloorToInt(CurrentComboCount / 10.0f);
        }

        void IncrementComboCount()
        {
            CurrentComboCount += 1;
            if (CurrentComboCount > _maxComboCount)
                _maxComboCount = CurrentComboCount;
        }

        void EndCombo()
        {
            CurrentComboCount = 0;
        }
    }
}