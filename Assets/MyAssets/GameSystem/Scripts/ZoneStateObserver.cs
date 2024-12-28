#nullable enable
using CodeRedCat._4kVectorLandscape.Demo.Scripts;
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
        BackGroundViewMono _backGroundViewMono;
        CameraScroll _cameraScroll;
        
        [Inject]
        public ZoneStateObserver(ZoneStateHolder zoneStateHolder,PlayerStateHolder playerStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,DisposeManager disposeManager,
            BackGroundViewMono backGroundViewMono,CameraScroll cameraScroll)
        {
            _playerStateHolder = playerStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            _backGroundViewMono = backGroundViewMono;
            _cameraScroll = cameraScroll;
            
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
            _backGroundViewMono.ChangeToZoneBackGround();
            _cameraScroll.StopCameraMove();
        }

        void ExecuteActionOnNotZone()
        {
            _playerStateHolder.ChangePlayerState(PlayerStateHolder.PlayerState.Default);
            _battleEnemySwitcher.ChangeWaitingEnemyCount(1);
            _backGroundViewMono.RevertCurrentBackGround();
            _cameraScroll.ReStartCameraMove();
        }
    }
}