#nullable enable
using System;
using UniRx;
using UnityEngine;

namespace Model.Player
{
    public class HiddenObjectStateHolder
    {
        public enum HiddenObject
        {
            Human,
            Money,
        }

        public IReadOnlyReactiveProperty<HiddenObject> CurrentHandState => _currentHandState;
        readonly ReactiveProperty<HiddenObject> _currentHandState = new (HiddenObject.Human);

        public void SwitchHiddenObject()
        {
            switch (_currentHandState.Value)
            {
                case HiddenObject.Human:
                    ChangeHiddenObject(HiddenObject.Money);
                    break;
                case HiddenObject.Money:
                    ChangeHiddenObject(HiddenObject.Human);
                    break;
                default:
                    Debug.LogWarning($"Invalid HiddenObject: {_currentHandState.Value}"); 
                    break;
            }
        }
        
        void ChangeHiddenObject(HiddenObject hiddenObject)
        {
            _currentHandState.Value = hiddenObject;
        }
    }
}