#nullable enable
using System.Collections.Generic;
using Model.GameSystem;
using MyAssets.Enemy.Scripts;
using UnityEngine;

namespace View
{
    public sealed class EnemyBabyViewMono : AbstractEnemyViewMono 
    {
        [SerializeField] SpriteRenderer spriteRenderer = null!;
        [SerializeField] List<Sprite> enemyAnimationSpriteList = null!;
        [SerializeField] Sprite defaultSprite = null!;
        [SerializeField] Sprite crySprite = null!;
        [SerializeField] Sprite confuseSprite = null!;
        [SerializeField] Sprite happySprite = null!;
        [SerializeField] Sprite superHappySprite = null!;

        public override void ChangeSpriteToCry()
        {
            spriteRenderer.sprite = crySprite;
        }

        public override void ChangeSpriteToConfuse()
        {
            spriteRenderer.sprite = confuseSprite;
        }

        public override void ChangeAnimationSprite(EvaluationData.Evaluation evaluation)
        {
            switch (evaluation)
            {
                case EvaluationData.Evaluation.Miss:
                    spriteRenderer.sprite = enemyAnimationSpriteList[0];
                    break;
                case EvaluationData.Evaluation.Good:
                    spriteRenderer.sprite = enemyAnimationSpriteList[1];
                    break;
                case EvaluationData.Evaluation.Excellent:
                    spriteRenderer.sprite = enemyAnimationSpriteList[2];
                    break;
            }
        }
        
        public override void ChangePosition(Vector2 position)
        {
            transform.position = position;
        }
        
    }
}