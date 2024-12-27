#nullable enable
using System;
using General;
using Model.Player;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class HandPresenter : IInitializable
    {
        readonly HandViewMono _handViewMono;
        readonly HandStateHolder _handStateHolder;
        readonly PlayerStateHolder _playerStateHolder;
        readonly DisposeManager _disposeManager;
        
        [Inject]
        public HandPresenter(HandViewMono handViewMono, HandStateHolder handStateHolder,
            PlayerStateHolder playerStateHolder,DisposeManager disposeManager)
        {
            _handViewMono = handViewMono;
            _handStateHolder = handStateHolder;
            _playerStateHolder = playerStateHolder;
            _disposeManager = disposeManager;
            
            SubscribeWithHandState();
            SubscribeWithPlayerState();
        }
        
        //エントリーポイント用
        public void Initialize(){}

        void SubscribeWithHandState()
        {
            _handStateHolder.ObserveEveryValueChanged(stateHolder => stateHolder.CurrentHandState)
                .Subscribe(handState =>  
                {
                    switch (handState)
                    {
                        case HandStateHolder.HandState.Open:
                            _handViewMono.MoveHandToOpen();
                            break;

                        case HandStateHolder.HandState.Close:
                            _handViewMono.MoveHandToClose();
                            break;
                        default:
                           Debug.LogWarning($"Invalid HandState: {handState}"); 
                            break; 
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }
        
        void SubscribeWithPlayerState()
        {
            _playerStateHolder
                .ObserveEveryValueChanged(stateHolder => stateHolder.CurrentPlayerState)
                .Subscribe(playerState =>  {
                    switch (playerState)
                    {
                        case PlayerStateHolder.PlayerState.Default:
                            _handViewMono.ChangeHandSpriteToDefault();
                            break;

                        case PlayerStateHolder.PlayerState.God:
                            _handViewMono.ChangeHandSpriteToGod();
                            break;
                        default:
                            Debug.LogWarning($"Invalid PlayerState: {playerState}");
                            break;
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
               
        }
        
    }
}