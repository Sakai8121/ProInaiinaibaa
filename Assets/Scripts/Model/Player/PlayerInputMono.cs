#nullable enable
using System;
using UnityEngine;
using VContainer;

namespace Model.Player
{
    public class PlayerInputMono:MonoBehaviour
    {
        HandStateHolder _handStateHolder = null!;
        HiddenObjectStateHolder _hiddenObjectStateHolder = null!;
        
        [Inject]
        public void Construct(HandStateHolder handStateHolder,HiddenObjectStateHolder hiddenObjectStateHolder)
        {
            _handStateHolder = handStateHolder;
            _hiddenObjectStateHolder = hiddenObjectStateHolder;
        }
        
        void Update()
        {
            CheckPlayerInput();
        }

        void CheckPlayerInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Close);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Open);
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                _hiddenObjectStateHolder.SwitchHiddenObject();
            }
        }
    }
}