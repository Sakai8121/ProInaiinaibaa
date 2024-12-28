#nullable enable
using System.Collections.Generic;
using Model.GameSystem;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class GameScoreHolder
    {
        public int GameScore { get; set; }
        public List<EvaluationData.Evaluation> EvaluationList { get; set; } = new();

        Dictionary<EvaluationData.Evaluation, int> _evaluationCount = new ()
        {
            { EvaluationData.Evaluation.Excellent, 0 },
            { EvaluationData.Evaluation.Good, 0 },
            { EvaluationData.Evaluation.Normal,0 },
            { EvaluationData.Evaluation.Miss, 0 }
        };
        Dictionary<EvaluationData.Evaluation, int> _evaluationScoreMap = new ()
        {
            { EvaluationData.Evaluation.Excellent, 10 },
            { EvaluationData.Evaluation.Good, 8 },
            { EvaluationData.Evaluation.Normal,1 },
            { EvaluationData.Evaluation.Miss, 0 }
        };
        
        public void AddScore(EvaluationData.Evaluation evaluation)
        {
            GameScore += _evaluationScoreMap[evaluation];
            _evaluationCount[evaluation] += 1;

            var newList = new List<EvaluationData.Evaluation>(EvaluationList);
            newList.Add(evaluation);
            EvaluationList = newList;
        }
    }
}