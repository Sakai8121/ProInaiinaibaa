#nullable enable
using System.Collections.Generic;
using DG.Tweening;
using Model.GameSystem;
using MyAssets.Enemy.Scripts;
using UnityEngine;

namespace View
{
    public sealed class EnemyAdultViewMono: AbstractEnemyViewMono
    {
        [SerializeField] SpriteRenderer spriteRenderer = null!;
        [SerializeField] List<Sprite> enemyAnimationSpriteList = null!;
        [SerializeField] Sprite defaultSprite = null!;
        [SerializeField] Sprite crySprite = null!;
        [SerializeField] Sprite confuseSprite = null!;
        [SerializeField] Sprite happySprite = null!;
        [SerializeField] Sprite superHappySprite = null!;

        Sequence? _movePositionSequence;

        public override void InitSprite()
        {
            transform.rotation = Quaternion.identity;
            spriteRenderer.sprite = defaultSprite;
        }

        public override void ChangeSpriteOrder(int order)
        {
            spriteRenderer.sortingOrder = order;
        }
        
        public override void ChangeSpriteByEvaluationResult(EvaluationData.Evaluation evaluation)
        {
            switch (evaluation)
            {
                case EvaluationData.Evaluation.Normal:
                    spriteRenderer.sprite = confuseSprite;
                    break;
                case EvaluationData.Evaluation.Good:
                    spriteRenderer.sprite = happySprite;
                    break;
                case EvaluationData.Evaluation.Excellent:
                    spriteRenderer.sprite = superHappySprite;
                    break;
                case EvaluationData.Evaluation.Miss:
                    spriteRenderer.sprite = crySprite;
                    break;
            }
        }

        public override void ChangeAnimationSprite(EvaluationData.Evaluation evaluation)
        {
            if (enemyAnimationSpriteList.Count < 4)
            {
                Debug.LogError("Not Enough Sprite");
                return;
            }
            
            switch (evaluation)
            {
                case EvaluationData.Evaluation.Normal:
                    spriteRenderer.sprite = enemyAnimationSpriteList[0];
                    break;
                case EvaluationData.Evaluation.Good:
                    spriteRenderer.sprite = enemyAnimationSpriteList[1];
                    break;
                case EvaluationData.Evaluation.Excellent:
                    spriteRenderer.sprite = enemyAnimationSpriteList[2];
                    break;
                case EvaluationData.Evaluation.Miss:
                    spriteRenderer.sprite = enemyAnimationSpriteList[3];
                    break;
            }
        }

        public override void ChangePosition(Vector2 position,bool requiresAnimation)
        {
            if (!requiresAnimation)
            {
                transform.position = position;
                return;
            }
            
            if(_movePositionSequence.IsActive() && _movePositionSequence.IsPlaying())
                _movePositionSequence.Kill();

            _movePositionSequence = DOTween.Sequence()
                .Append(transform.DOJump(position, 0.5f,3, EnemyDefaultParameter.MoveAnimationSpeed))
                .OnKill(() => transform.position = position)
                .SetLink(gameObject);
        }
        
        public override Sequence Blown()
        {
            if(_movePositionSequence.IsActive() && _movePositionSequence.IsPlaying())
                _movePositionSequence.Kill();

            return _movePositionSequence = DOTween.Sequence()
                .Append(transform.DOMove
                    (EnemyDefaultParameter.BlownPosition, EnemyDefaultParameter.BlownAnimationSpeed))
                .Join(transform.DORotate
                    (EnemyDefaultParameter.RotateValue, EnemyDefaultParameter.BlownAnimationSpeed))
                //.OnKill(() => transform.position = EnemyDefaultParameter.GeneratePosition)
                .SetLink(gameObject);
        }
    }
}