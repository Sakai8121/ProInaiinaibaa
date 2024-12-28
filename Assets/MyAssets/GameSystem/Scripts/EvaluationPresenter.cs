#nullable enable
using General;
using Model.Enemy;
using Model.Player;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationPresenter : IInitializable
    {
        EvaluationViewMono _evaluationViewMono;
        EvaluationTimeCounter _evaluationTimeCounter;
        EvaluationTargetTimeHolder _evaluationTargetTimeHolder;
        DisposeManager _disposeManager;
        ZoneStateHolder _zoneStateHolder;
        BattleEnemySwitcher _battleEnemySwitcher;
        EvaluationDecider _evaluationDecider;
        HandStateHolder _handStateHolder;
        
        [Inject]
        public EvaluationPresenter(EvaluationViewMono evaluationViewMono, EvaluationTimeCounter evaluationTimeCounter,
            EvaluationTargetTimeHolder evaluationTargetTimeHolder,DisposeManager disposeManager,
            ZoneStateHolder zoneStateHolder,BattleEnemySwitcher battleEnemySwitcher,
            EvaluationDecider evaluationDecider,HandStateHolder handStateHolder)
        {
            _evaluationViewMono = evaluationViewMono;
            _evaluationTimeCounter = evaluationTimeCounter;
            _evaluationTargetTimeHolder = evaluationTargetTimeHolder;
            _disposeManager = disposeManager;
            _zoneStateHolder = zoneStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            _evaluationDecider = evaluationDecider;
            _handStateHolder = handStateHolder;
            
            SubscribeWithTimer();
        }
        
        //エントリーポイント用
        public void Initialize(){}

        void SubscribeWithTimer()
        {
            _evaluationTimeCounter
                .ObserveEveryValueChanged(timeCounter => timeCounter.CurrentTimer)
                .Subscribe(currentTimer =>
                {
                    var currentTargetTime = _evaluationTargetTimeHolder.CurrentTargetTime;
                    var gaugeRate = currentTimer / currentTargetTime;
                    _evaluationViewMono.ChangeGaugeView(gaugeRate);
                    
                    if (_zoneStateHolder.IsZoneState())
                    {
                        _evaluationViewMono.ChangeGaugeToInZone();
                    }
                    else
                    {
                        _evaluationViewMono.ChangeGaugeToDefault();
                    }
                    
                    CheckEvaluationForEnemySprite();
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }

        void CheckEvaluationForEnemySprite()
        {
            if(_handStateHolder.CurrentHandState == HandStateHolder.HandState.Open)
                return;
            
            var evaluation = _evaluationDecider.DecideEvaluation();
            var battleEnemy = _battleEnemySwitcher.CurrentBattleEnemy;
            battleEnemy.Do(enemy=>enemy.EnemyViewMono.ChangeAnimationSprite(evaluation));
        }
    }
}