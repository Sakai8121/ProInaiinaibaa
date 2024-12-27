#nullable enable
using Model.Enemy;
using Model.Player;
using MyAssets.Enemy.Scripts;
using MyAssets.Player.Scripts;
using Presenter;
using VContainer;
using VContainer.Unity;
using View;

namespace General
{
    public class PlaySceneLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // PureC#の登録
            builder.Register<HandStateHolder>(Lifetime.Scoped);
            builder.Register<PlayerStateHolder>(Lifetime.Scoped);
            builder.Register<HiddenObjectStateHolder>(Lifetime.Scoped);
            builder.Register<DisposeManager>(Lifetime.Scoped);
            builder.Register<EnemyObjectPool>(Lifetime.Scoped);
            builder.Register<BattleEnemySwitcher>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<HandPresenter>();
            builder.RegisterEntryPoint<EnemyPositionPresenter>();
            builder.RegisterEntryPoint<HiddenObjectPresenter>();
            builder.RegisterEntryPoint<PlayerPresenter>();
            
            // MonoBehaviourの登録
            builder.RegisterComponentInHierarchy<HandViewMono>();
            builder.RegisterComponentInHierarchy<PlayerInputMono>();
            builder.RegisterComponentInHierarchy<EnemyGeneratorMono>();
            builder.RegisterComponentInHierarchy<HiddenObjectViewMono>();
            builder.RegisterComponentInHierarchy<PlayerViewMono>();
        }
    }
}