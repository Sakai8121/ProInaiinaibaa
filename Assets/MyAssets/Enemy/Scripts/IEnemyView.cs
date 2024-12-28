using Model.GameSystem;

namespace MyAssets.Enemy.Scripts
{
    public interface IEnemyView
    {
        void InitSprite();
        void ChangeSpriteOrder(int order);
        void ChangeSpriteByEvaluationResult(EvaluationData.Evaluation evaluation);
        void ChangeAnimationSprite(EvaluationData.Evaluation evaluation);
    }
}