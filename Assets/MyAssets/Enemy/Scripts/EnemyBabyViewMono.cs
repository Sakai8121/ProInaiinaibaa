#nullable enable
using System.Collections.Generic;
using Model.GameSystem;
using MyAssets.Enemy.Scripts;
using UnityEngine;

namespace View
{
    public class EnemyBabyViewMono : MonoBehaviour,IEnemyView,IEnemyChangeTransformView
    {
        [SerializeField] SpriteRenderer spriteRenderer = null!;
        [SerializeField] List<Sprite> enemyAnimationSpriteList = null!;
        [SerializeField] Sprite crySprite = null!;
        [SerializeField] Sprite confuseSprite = null!;

        public void ChangeSpriteToCry()
        {
            spriteRenderer.sprite = crySprite;
        }

        public void ChangeSpriteToConfuse()
        {
            spriteRenderer.sprite = confuseSprite;
        }

        public void ChangeAnimationSprite(EvaluationData.Evaluation evaluation)
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
        
        public void ChangePosition(Vector2 position)
        {
            transform.position = position;
        }
        
    }
}