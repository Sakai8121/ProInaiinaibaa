#nullable enable
using System.Linq;
using DG.Tweening;
using General;
using MobileLibrary.Function;
using Model.Enemy;
using MyAssets.Enemy.Scripts;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presenter
{
    public class EnemyPositionPresenter: IInitializable
    {
        readonly BattleEnemySwitcher _battleEnemySwitcher;
        readonly DisposeManager _disposeManager;
        readonly EnemyObjectPool _enemyObjectPool;

        readonly Vector2 _battleEnemyPosition = new Vector2(3, -3);
        readonly Vector2 _firstWaitingEnemyPosition = new Vector2(7, -3);
        
        [Inject]
        public EnemyPositionPresenter(BattleEnemySwitcher battleEnemySwitcher,DisposeManager disposeManager,
            EnemyObjectPool enemyObjectPool)
        {
            _battleEnemySwitcher = battleEnemySwitcher;
            _disposeManager = disposeManager;
            _enemyObjectPool = enemyObjectPool;

            SubscribeWithCurrentBattleEnemy();
        }
        
        //エントリーポイント用
        public void Initialize(){}

        void SubscribeWithCurrentBattleEnemy()
        {
            _battleEnemySwitcher
                .ObserveEveryValueChanged(switcher => switcher.CurrentBattleEnemy)
                .Subscribe(enemy =>
                {
                    enemy.Do(enemy =>
                    {
                        enemy.EnemyViewMono.ChangePosition(_battleEnemyPosition,true);
                    });
                })
                .AddTo(_disposeManager.CompositeDisposable);
            
            _battleEnemySwitcher
                .ObserveEveryValueChanged(switcher => switcher.DefeatedEnemy)
                .Subscribe(enemy =>
                {
                    enemy.Do(enemyMono =>
                    {
                        var blownSequence = enemyMono.EnemyViewMono.Blown();
                        blownSequence
                            .OnKill(()=>DisActiveEnemy(enemyMono));
                    });
                })
                .AddTo(_disposeManager.CompositeDisposable);
            
            _battleEnemySwitcher
                .ObserveEveryValueChanged(switcher => switcher.WaitingEnemyList)
                .Subscribe(waitingEnemyList =>
                {
                    foreach (var waitingEnemy in waitingEnemyList.Select((enemyOption, index) => new { enemyOption, index }))
                    {
                        waitingEnemy.enemyOption.Do(enemy =>
                        {
                            var generatePosition =
                                _firstWaitingEnemyPosition + new Vector2(0.5f, 0) * waitingEnemy.index;
                            enemy.EnemyViewMono.ChangePosition(generatePosition,true);
                        });
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }
        
        void DisActiveEnemy(Option<EnemyMono> enemyOption)
        {
            enemyOption.Do(enemyMono=>
                enemyMono.EnemyViewMono.transform.position = EnemyDefaultParameter.GeneratePosition);
            _enemyObjectPool.AddDestroyedEnemy(enemyOption);
        }
        
    }
}