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

        public PlayerState CurrentPlayerState { get; set; }
        
        public void ChangePlayerState(PlayerState playerState)
        {
            CurrentPlayerState = playerState;
        }
    }
}