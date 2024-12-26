#nullable enable
using Model.GameSystem;
using UnityEngine;

namespace MyAssets.Enemy.Scripts
{
    public enum EnemyKind
    {
        Adult,
        Baby
    }

    public class EnemyView : IEnemyView, IEnemyChangeTransformView
    {
        public void ChangeSpriteToCry() => _enemyView.ChangeSpriteToCry();
        public void ChangeSpriteToConfuse() => _enemyView.ChangeSpriteToConfuse();
        public void ChangeAnimationSprite(EvaluationData.Evaluation evaluation) => _enemyView.ChangeAnimationSprite(evaluation);
        public void ChangePosition(Vector2 position) => _enemyChangeTransformView.ChangePosition(position);
        readonly IEnemyView _enemyView;
        readonly IEnemyChangeTransformView _enemyChangeTransformView;

        public EnemyView(IEnemyView view, IEnemyChangeTransformView enemyChangeTransformView)
        {
            _enemyView = view;
            _enemyChangeTransformView = enemyChangeTransformView;
        }
    }
}