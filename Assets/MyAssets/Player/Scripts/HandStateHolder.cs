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

        public HandState CurrentHandState { get; set; }

        public void ChangeHandState(HandState handState)
        {
            CurrentHandState = handState;
        }
    }
}