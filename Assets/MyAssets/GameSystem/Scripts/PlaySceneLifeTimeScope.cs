#nullable enable
using CodeRedCat._4kVectorLandscape.Demo.Scripts;
using Model.Enemy;
using Model.Player;
using MyAssets.Enemy.Scripts;
using MyAssets.GameSystem.Scripts;
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
            builder.Register<EvaluationDecider>(Lifetime.Scoped);
            builder.Register<ZoneStateHolder>(Lifetime.Scoped);
            builder.Register<GameScoreHolder>(Lifetime.Scoped);
            builder.Register<TimeStateHolder>(Lifetime.Scoped);
            builder.Register<GameStartFlagHolder>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<HandPresenter>();
            builder.RegisterEntryPoint<EnemyPositionPresenter>();
            builder.RegisterEntryPoint<HiddenObjectPresenter>();
            builder.RegisterEntryPoint<PlayerPresenter>();
            builder.RegisterEntryPoint<EvaluationPresenter>();
            builder.RegisterEntryPoint<ZoneStateObserver>();
            builder.RegisterEntryPoint<TimeStatePresenter>();
            builder.RegisterEntryPoint<TimeAndScorePresenter>();
            builder.RegisterEntryPoint<EvaluationTargetTimeHolder>().AsSelf();
            builder.RegisterEntryPoint<EvaluationTimeCounter>().AsSelf();
            builder.RegisterEntryPoint<JudgeZoneExecutor>().AsSelf();
            builder.RegisterEntryPoint<GameTimeHolder>().AsSelf();
            
            // MonoBehaviourの登録
            builder.RegisterComponentInHierarchy<HandViewMono>();
            builder.RegisterComponentInHierarchy<PlayerInputMono>();
            builder.RegisterComponentInHierarchy<EnemyGeneratorMono>();
            builder.RegisterComponentInHierarchy<HiddenObjectViewMono>();
            builder.RegisterComponentInHierarchy<PlayerViewMono>();
            builder.RegisterComponentInHierarchy<EvaluationGaugeViewMono>();
            builder.RegisterComponentInHierarchy<BackGroundViewMono>();
            builder.RegisterComponentInHierarchy<EvaluationTextViewMono>();
            builder.RegisterComponentInHierarchy<CameraScroll>();
            builder.RegisterComponentInHierarchy<ScoreTextViewMono>();
            builder.RegisterComponentInHierarchy<TimeTextViewMono>();
            builder.RegisterComponentInHierarchy<SoundManager>();
            builder.RegisterComponentInHierarchy<ResultViewMono>();
        }
    }
}