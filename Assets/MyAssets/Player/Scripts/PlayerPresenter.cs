#nullable enable
using General;
using Model.Player;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace MyAssets.Player.Scripts
{
    public class PlayerPresenter : IInitializable
    {
        PlayerStateHolder _playerStateHolder;
        PlayerViewMono _playerViewMono;
        DisposeManager _disposeManager;
        
        [Inject]
        public PlayerPresenter(PlayerStateHolder playerStateHolder,PlayerViewMono playerViewMono,
            DisposeManager disposeManager)
        {
            _playerStateHolder = playerStateHolder;
            _playerViewMono = playerViewMono;
            _disposeManager = disposeManager;

            SubscribeWithPlayerState();
        }

        void SubscribeWithPlayerState()
        {
            _playerStateHolder
                .ObserveEveryValueChanged(stateHolder => stateHolder.CurrentPlayerState)
                .Subscribe(playerState => _playerViewMono.ChangeSprite(playerState))
                .AddTo(_disposeManager.CompositeDisposable);
        }
        
        // エントリーポイント用
        public void Initialize(){}
    }
}