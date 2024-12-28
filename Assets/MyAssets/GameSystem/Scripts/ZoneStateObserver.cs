#nullable enable
using General;
using Model.Enemy;
using Model.Player;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class ZoneStateObserver : IInitializable
    {
        PlayerStateHolder _playerStateHolder;
        BattleEnemySwitcher _battleEnemySwitcher;
        
        [Inject]
        public ZoneStateObserver(ZoneStateHolder zoneStateHolder,PlayerStateHolder playerStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,DisposeManager disposeManager)
        {
            _playerStateHolder = playerStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            
            zoneStateHolder.ObserveEveryValueChanged(holder => holder.CurrentZoneState)
                .Subscribe(currentZoneState =>
                {
                    switch (currentZoneState)
                    {
                        case ZoneState.InZone:
                            ExecuteActionOnInZone();
                            break;
                        case ZoneState.NotZone:
                            ExecuteActionOnNotZone();
                            break;
                    }
                })
                .AddTo(disposeManager.CompositeDisposable);
        }
        
        //エントリーポイント用
        public void Initialize(){}

        void ExecuteActionOnInZone()
        {
            _playerStateHolder.ChangePlayerState(PlayerStateHolder.PlayerState.God);
            _battleEnemySwitcher.ChangeWaitingEnemyCount(3);
        }

        void ExecuteActionOnNotZone()
        {
            _playerStateHolder.ChangePlayerState(PlayerStateHolder.PlayerState.Default);
            _battleEnemySwitcher.ChangeWaitingEnemyCount(1);
        }
    }
}