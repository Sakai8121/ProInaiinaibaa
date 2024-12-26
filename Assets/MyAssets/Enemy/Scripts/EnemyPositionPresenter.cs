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
        readonly Vector2 _blownEnemyPosition = new Vector2(50, 10);
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
            _battleEnemySwitcher.CurrentBattleEnemy
                .Subscribe(battleEnemy =>
                {
                    battleEnemy.Do(enemy =>
                    {
                        enemy.EnemyChangeTransformView.ChangePosition(_battleEnemyPosition);
                    });
                })
                .AddTo(_disposeManager.CompositeDisposable);
            
            _battleEnemySwitcher.PreBattleEnemy
                .Subscribe(preBattleEnemy =>
                {
                    preBattleEnemy.Do(enemy =>
                    {
                        enemy.EnemyChangeTransformView.ChangePosition(_blownEnemyPosition);
                    });
                })
                .AddTo(_disposeManager.CompositeDisposable);
            
            _battleEnemySwitcher.WaitingEnemyList
                .Subscribe(waitingEnemyList =>
                {
                    foreach (var waitingEnemy in waitingEnemyList.Select((enemyOption, index) => new { enemyOption, index }))
                    {
                        waitingEnemy.enemyOption.Do(enemy =>
                        {
                            var generatePosition =
                                _firstWaitingEnemyPosition + new Vector2(0.5f, 0) * waitingEnemy.index;
                            enemy.EnemyChangeTransformView.ChangePosition(generatePosition);
                        });
                    }
                })
                .AddTo(_disposeManager.CompositeDisposable);
        }
    }
}