#nullable enable
using System;
using System.Threading;
using CodeRedCat._4kVectorLandscape.Demo.Scripts;
using Cysharp.Threading.Tasks;
using General;
using Model.Enemy;
using Model.Player;
using UniRx;
using VContainer;
using VContainer.Unity;
using CancellationTokenSource = System.Threading.CancellationTokenSource;

namespace MyAssets.GameSystem.Scripts
{
    public class ZoneStateObserver : IInitializable
    {
        PlayerStateHolder _playerStateHolder;
        BattleEnemySwitcher _battleEnemySwitcher;
        BackGroundViewMono _backGroundViewMono;
        CameraScroll _cameraScroll;
        ZoneStateHolder _zoneStateHolder;
        CancellationTokenSource _cancellationTokenSource;
        DisposeManager _disposeManager;
        
        [Inject]
        public ZoneStateObserver(ZoneStateHolder zoneStateHolder,PlayerStateHolder playerStateHolder,
            BattleEnemySwitcher battleEnemySwitcher,DisposeManager disposeManager,
            BackGroundViewMono backGroundViewMono,CameraScroll cameraScroll)
        {
            _playerStateHolder = playerStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
            _backGroundViewMono = backGroundViewMono;
            _cameraScroll = cameraScroll;
            _zoneStateHolder = zoneStateHolder;
            _disposeManager = disposeManager;
            
            _cancellationTokenSource = new CancellationTokenSource();
            
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

            _cancellationTokenSource = new CancellationTokenSource();
            _disposeManager.CompositeDisposable.Add(_cancellationTokenSource);
            WaitInZoneTimeUniTask(_cancellationTokenSource);
        }

        void ExecuteActionOnNotZone()
        {
            _cancellationTokenSource.Cancel();
            _playerStateHolder.ChangePlayerState(PlayerStateHolder.PlayerState.Default);
            _battleEnemySwitcher.ChangeWaitingEnemyCount(1);
            _backGroundViewMono.RevertCurrentBackGround();
            _cameraScroll.ReStartCameraMove();
        }

        async void WaitInZoneTimeUniTask(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                // 5秒待機中にキャンセル可能
                await UniTask.Delay(TimeSpan.FromSeconds(5.0f), cancellationToken: cancellationTokenSource.Token);

                // キャンセルされなかった場合の処理
                _zoneStateHolder.EndZoneMode();
            }
            catch (OperationCanceledException)
            {
                // キャンセル時の処理
            }
        }
    }
}