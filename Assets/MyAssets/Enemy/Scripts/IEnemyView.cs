using Model.GameSystem;

namespace MyAssets.Enemy.Scripts
{
    public interface IEnemyView
    {
        void ChangeSpriteToCry();
        void ChangeSpriteToConfuse();
        void ChangeAnimationSprite(EvaluationData.Evaluation evaluation);
    }
}