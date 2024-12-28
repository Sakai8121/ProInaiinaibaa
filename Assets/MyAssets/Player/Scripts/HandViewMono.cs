#nullable enable
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class HandViewMono:MonoBehaviour
    {
        [SerializeField] Transform rightHandTransform = null!;
        [SerializeField] Transform leftHandTransform = null!;
        [SerializeField] SpriteRenderer rightHandSpriteRenderer = null!;
        [SerializeField] SpriteRenderer leftHandSpriteRenderer = null!;
        [SerializeField] GameObject rightGodHandsParent = null!;
        [SerializeField] GameObject leftGodHandsParent = null!;

        Sequence? _handTransformSequence;
        readonly Vector3 _rightOpenRotateVector = new Vector3(0, 0, 0);
        readonly Vector3 _rightCloseRotateVector = new Vector3(0, 0, 45);
        readonly Vector3 _leftOpenRotateVector = new Vector3(0, 180, 0);
        readonly Vector3 _leftCloseRotateVector = new Vector3(0, 180, 45);
        
        public void MoveHandToClose()
        {
            if (_handTransformSequence.IsActive() && _handTransformSequence.IsPlaying())
            {
                _handTransformSequence.Kill();
                rightHandTransform.rotation = Quaternion.Euler(_rightOpenRotateVector);
                leftHandTransform.rotation = Quaternion.Euler(_leftOpenRotateVector);
            }
            _handTransformSequence = DOTween.Sequence()
                .Append(rightHandTransform.DORotate(_rightCloseRotateVector, 0.2f))
                .Join(leftHandTransform.DORotate(_leftCloseRotateVector, 0.2f))
                .SetLink(gameObject);
        }

        public void MoveHandToOpen()
        {
            if (_handTransformSequence.IsActive() && _handTransformSequence.IsPlaying())
            {
                _handTransformSequence.Kill();
                rightHandTransform.rotation = Quaternion.Euler(_rightCloseRotateVector);
                leftHandTransform.rotation = Quaternion.Euler(_leftCloseRotateVector);
            }
            _handTransformSequence = DOTween.Sequence()
                .Append(rightHandTransform.DORotate(_rightOpenRotateVector, 0.2f))
                .Join(leftHandTransform.DORotate(_leftOpenRotateVector, 0.2f))
                .SetLink(gameObject);
        }

        public void ChangeHandSpriteToDefault()
        {
            rightHandSpriteRenderer.color = Color.white;
            leftHandSpriteRenderer.color = Color.white;
            rightGodHandsParent.SetActive(false);
            leftGodHandsParent.SetActive(false);
        }

        public void ChangeHandSpriteToGod()
        {
            rightHandSpriteRenderer.color = Color.yellow;
            leftHandSpriteRenderer.color = Color.yellow;
            rightGodHandsParent.SetActive(true);
            leftGodHandsParent.SetActive(true);
        }
    }
}