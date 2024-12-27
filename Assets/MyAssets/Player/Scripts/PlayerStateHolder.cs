#nullable enable
using UniRx;

namespace Model.Player
{
    public class PlayerStateHolder
    {
        public enum PlayerState
        {
            Default,
            God,
        }

        public IReadOnlyReactiveProperty<PlayerState> CurrentPlayerState => _currentPlayerState;
        readonly ReactiveProperty<PlayerState> _currentPlayerState = new (PlayerState.Default);
        
        public void ChangePlayerState(PlayerState playerState)
        {
            _currentPlayerState.Value = playerState;
        }
    }
}