#nullable enable
using System.Collections.Generic;
using Model.Enemy;
using Model.GameSystem;
using Model.Player;
using MyAssets.Enemy.Scripts;
using UnityEngine;
using VContainer;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationDecider
    {
        EvaluationTimeCounter _evaluationTimeCounter;
        EvaluationTargetTimeHolder _evaluationTargetTimeHolder;
        ZoneStateHolder _zoneStateHolder;
        BattleEnemySwitcher _battleEnemySwitcher;
        HiddenObjectStateHolder _hiddenObjectStateHolder;
        
        [Inject]
        public EvaluationDecider(EvaluationTimeCounter evaluationTimeCounter,
            EvaluationTargetTimeHolder evaluationTargetTimeHolder,ZoneStateHolder zoneStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,HiddenObjectStateHolder hiddenObjectStateHolder)
        {
            _evaluationTimeCounter = evaluationTimeCounter;
            _evaluationTargetTimeHolder = evaluationTargetTimeHolder;
            _zoneStateHolder = zoneStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            _hiddenObjectStateHolder = hiddenObjectStateHolder;
        }
        
        public EvaluationData.Evaluation DecideEvaluation()
        {
            if (!IsSuitableHiddenObject())
            {
                return EvaluationData.Evaluation.Normal;
            }
            
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

        bool IsSuitableHiddenObject()
        {
            Dictionary<EnemyKind, HiddenObjectStateHolder.HiddenObject> suitableHiddenObjectMap = new ()
            {
                { EnemyKind.Adult, HiddenObjectStateHolder.HiddenObject.Money },
                { EnemyKind.Baby, HiddenObjectStateHolder.HiddenObject.Human },
            };
            
            return _battleEnemySwitcher.CurrentBattleEnemy.Match(
                () => false,
                enemy => suitableHiddenObjectMap[enemy.EnemyKind] == _hiddenObjectStateHolder.CurrentHiddenObject
            );
        }
    }
}