#nullable enable
using DG.Tweening;
using Model.GameSystem;
using MyAssets.Enemy.Scripts;
using UnityEngine;

namespace View
{
    public abstract class AbstractEnemyViewMono : MonoBehaviour,IEnemyView,IEnemyChangeTransformView
    {
        public abstract void InitSprite();
        public abstract void ChangeSpriteByEvaluationResult(EvaluationData.Evaluation evaluation);
        public abstract void ChangeAnimationSprite(EvaluationData.Evaluation evaluation);
        public abstract void ChangePosition(Vector2 position,bool requiresAnimation);
        public abstract Sequence Blown();
    }
}