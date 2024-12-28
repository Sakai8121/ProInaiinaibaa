#nullable enable
using System;
using MyAssets.GameSystem.Scripts;
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
                    SoundManager.Instance.PlaySEOneShot(SESoundData.SE.GoMoneyMode);
                    break;
                case HiddenObject.Money:
                    ChangeHiddenObject(HiddenObject.Human);
                    SoundManager.Instance.PlaySEOneShot(SESoundData.SE.GoHumanMode);
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