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

        public HiddenObject CurrentHiddenObject { get; set; }

        public void SwitchHiddenObject()
        {
            switch (CurrentHiddenObject)
            {
                case HiddenObject.Human:
                    ChangeHiddenObject(HiddenObject.Money);
                    break;
                case HiddenObject.Money:
                    ChangeHiddenObject(HiddenObject.Human);
                    break;
                default:
                    Debug.LogWarning($"Invalid HiddenObject: {CurrentHiddenObject}"); 
                    break;
            }
        }
        
        void ChangeHiddenObject(HiddenObject hiddenObject)
        {
            CurrentHiddenObject = hiddenObject;
        }
    }
}