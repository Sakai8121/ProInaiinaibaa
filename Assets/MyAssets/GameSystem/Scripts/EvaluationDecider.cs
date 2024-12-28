#nullable enable
using Model.GameSystem;
using VContainer;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationDecider
    {
        EvaluationTimeCounter _evaluationTimeCounter;
        EvaluationTargetTimeHolder _evaluationTargetTimeHolder;
        ZoneStateHolder _zoneStateHolder;
        
        [Inject]
        public EvaluationDecider(EvaluationTimeCounter evaluationTimeCounter,
            EvaluationTargetTimeHolder evaluationTargetTimeHolder,ZoneStateHolder zoneStateHolder)
        {
            _evaluationTimeCounter = evaluationTimeCounter;
            _evaluationTargetTimeHolder = evaluationTargetTimeHolder;
            _zoneStateHolder = zoneStateHolder;
        }

        public EvaluationData.Evaluation DecideEvaluation()
        {
            if (_zoneStateHolder.IsZoneState())
                return EvaluationData.Evaluation.Excellent;
            
            const float missRate = 1.0f;
            const float excellentRate = 0.9f;
            const float goodRate = 0.75f;
            
            float timeRate = _evaluationTimeCounter.CurrentTimer / _evaluationTargetTimeHolder.CurrentTargetTime;
            if (timeRate > missRate)
                return EvaluationData.Evaluation.Miss;
            else if (timeRate > excellentRate)
                return EvaluationData.Evaluation.Excellent;
            else if (timeRate > goodRate)
                return EvaluationData.Evaluation.Good;
            else
                return EvaluationData.Evaluation.Normal;

        }
    }
}