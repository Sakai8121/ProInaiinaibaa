#nullable enable
using UniRx;
using UnityEngine;

namespace Model.Player
{
    public class HandStateHolder
    {
        public enum HandState
        {
            Open,
            Close,
        }

        public IReadOnlyReactiveProperty<HandState> CurrentHandState => _currentHandState;
        readonly ReactiveProperty<HandState> _currentHandState = new (HandState.Open);

        public void ChangeHandState(HandState handState)
        {
            Debug.Log($"ChangeTo {handState}");
            _currentHandState.Value = handState;
        }
    }
}