#nullable enable
using System;
using Model.Enemy;
using UnityEngine;
using VContainer;

namespace Model.Player
{
    public class PlayerInputMono:MonoBehaviour
    {
        HandStateHolder _handStateHolder = null!;
        HiddenObjectStateHolder _hiddenObjectStateHolder = null!;
        BattleEnemySwitcher _battleEnemySwitcher = null!;
        
        [Inject]
        public void Construct(HandStateHolder handStateHolder,HiddenObjectStateHolder hiddenObjectStateHolder,
            BattleEnemySwitcher battleEnemySwitcher)
        {
            _handStateHolder = handStateHolder;
            _hiddenObjectStateHolder = hiddenObjectStateHolder;
            _battleEnemySwitcher = battleEnemySwitcher;
        }
        
        void Update()
        {
            CheckPlayerInput();
        }

        void CheckPlayerInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Close);
            }
            
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return))
            {
                _handStateHolder.ChangeHandState(HandStateHolder.HandState.Open);
                _battleEnemySwitcher.SwitchToNextEnemy();
            }
            
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
            {
                _hiddenObjectStateHolder.SwitchHiddenObject();
            }
        }
    }
}