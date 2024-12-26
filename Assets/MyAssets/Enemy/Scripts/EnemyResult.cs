#nullable enable
namespace MyAssets.Enemy.Scripts
{
    public class EnemyResult
    {
        public enum EnemyKind
        {
            Adult,
            Baby
        }
        
        public IEnemyChangeTransformView EnemyChangeTransformView { get; }
        public IEnemyView EnemyView { get; }

        public EnemyResult(IEnemyChangeTransformView transformView, IEnemyView view)
        {
            EnemyChangeTransformView = transformView;
            EnemyView = view;
        }
    }
}