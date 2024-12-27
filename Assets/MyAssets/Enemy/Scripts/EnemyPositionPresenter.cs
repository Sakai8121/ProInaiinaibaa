#nullable enable
using System.Linq;
using General;
using Model.Enemy;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presenter
{
    public class EnemyPositionPresenter: IInitializable
    {
        BattleEnemySwitcher _battleEnemySwitcher;
        DisposeManager _disposeManager;

        readonly Vector2 _battleEnemyPosition = new Vector2(3, -3);
        readonly Vector2 _firstWaitingEnemyPosition = new Vector2(6, -3);
        
        [Inject]
        public EnemyPositionPresenter(BattleEnemySwitcher battleEnemySwitcher,DisposeManager disposeManager)
        {
            _battleEnemySwitcher = battleEnemySwitcher;
            _disposeManager = disposeManager;

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
                        enemy.EnemyViewMono.ChangePosition(_battleEnemyPosition);
                    });
                })
                .AddTo(_disposeManager.CompositeDisposable);
            
            _battleEnemySwitcher
                .ObserveEveryValueChanged(switcher => switcher.DefeatedEnemy)
                .Subscribe(enemy =>
                {
                    enemy.Do(enemy =>
                    {
                        enemy.EnemyViewMono.Blown();
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
                            enemy.EnemyViewMono.ChangePosition(generatePosition);
                        });
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }
    }
}