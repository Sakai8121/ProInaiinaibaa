#nullable enable
namespace MyAssets.Enemy.Scripts
{
    public enum EnemyKind
    {
        Adult,
        Baby
    }
    public class EnemyResult
    {
        public IEnemyChangeTransformView EnemyChangeTransformView { get; }
        public IEnemyView EnemyView { get; }

        public EnemyResult(IEnemyChangeTransformView transformView, IEnemyView view)
        {
            EnemyChangeTransformView = transformView;
            EnemyView = view;
        }
    }
}