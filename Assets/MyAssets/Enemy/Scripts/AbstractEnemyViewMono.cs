#nullable enable
using Model.GameSystem;
using MyAssets.Enemy.Scripts;
using UnityEngine;

namespace View
{
    public abstract class AbstractEnemyViewMono :  MonoBehaviour,IEnemyView,IEnemyChangeTransformView
    {
        public abstract void ChangeSpriteToCry();
        public abstract void ChangeSpriteToConfuse();
        public abstract void ChangeAnimationSprite(EvaluationData.Evaluation evaluation);
        public abstract void ChangePosition(Vector2 position);
    }
}