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
        HandViewMono _handViewMono;
        HandStateHolder _handStateHolder;
        PlayerStateHolder _playerStateHolder;
        DisposeManager _disposeManager;
        
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
            _handStateHolder.CurrentHandState
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case HandStateHolder.HandState.Open:
                            _handViewMono.MoveHandToOpen();
                            break;

                        case HandStateHolder.HandState.Close:
                            _handViewMono.MoveHandToClose();
                            break;
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }
        
        void SubscribeWithPlayerState()
        {
            _playerStateHolder.CurrentPlayerState
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case PlayerStateHolder.PlayerState.Default:
                            _handViewMono.ChangeHandSpriteToDefault();
                            break;

                        case PlayerStateHolder.PlayerState.God:
                            _handViewMono.ChangeHandSpriteToGod();
                            break;
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }
        
    }
}