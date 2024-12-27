#nullable enable
using DG.Tweening;
using Model.Player;
using UnityEngine;

namespace MyAssets.Player.Scripts
{
    public class HiddenObjectViewMono:MonoBehaviour
    {
        [SerializeField] Transform moneyTransform = null!;

        Sequence? _moneyAnimationSequence;
        
        readonly Vector3 _defaultSize = new (0.8f, 1.2f, 1);
        readonly Vector3 _dismissSize = new (0, 0, 1);
        readonly float _moneyAppearAnimationTime = 0.1f;
        readonly float _moneyDismissAnimationTime = 0.1f;
        
        public void ChangeHiddenObjectPosition(HiddenObjectStateHolder.HiddenObject hiddenObject)
        {
            if(_moneyAnimationSequence.IsActive() && _moneyAnimationSequence.IsPlaying())
                _moneyAnimationSequence.Kill();
            
            switch (hiddenObject)
            {
                case HiddenObjectStateHolder.HiddenObject.Money:
                    _moneyAnimationSequence = DOTween.Sequence()
                        .Append(moneyTransform.DOScale(_defaultSize, _moneyAppearAnimationTime))
                        .SetLink(gameObject)
                        .OnKill(() => moneyTransform.localScale = _defaultSize);
                    break;
                case HiddenObjectStateHolder.HiddenObject.Human:
                    _moneyAnimationSequence = DOTween.Sequence()
                        .Append(moneyTransform.DOScale(_dismissSize, _moneyDismissAnimationTime))
                        .SetLink(gameObject)
                        .OnKill(() => moneyTransform.localScale = _dismissSize);
                    break;
            }
        }
    }
}