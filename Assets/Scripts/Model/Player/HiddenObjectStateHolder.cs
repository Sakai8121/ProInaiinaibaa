#nullable enable
using UniRx;

namespace Model.Player
{
    public class HiddenObjectStateHolder
    {
        public enum HiddenObject
        {
            Human,
            Money
        }

        public IReadOnlyReactiveProperty<HiddenObject> CurrentHandState => _currentHandState;
        readonly ReactiveProperty<HiddenObject> _currentHandState = new (HiddenObject.Human);

        public void SwitchHiddenObject()
        {
            if(_currentHandState.Value == HiddenObject.Human)
                ChangeHiddenObject(HiddenObject.Money);
            else if(_currentHandState.Value == HiddenObject.Money)
                ChangeHiddenObject(HiddenObject.Human);
        }
        
        void ChangeHiddenObject(HiddenObject hiddenObject)
        {
            _currentHandState.Value = hiddenObject;
        }
    }
}