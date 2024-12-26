#nullable enable
using System.Collections.Generic;
using MobileLibrary.Function;
using MyAssets.Enemy.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace Model.Enemy
{
    public class BattleEnemySwitcher
    {
        public IReadOnlyReactiveProperty<Option<EnemyView>> CurrentBattleEnemy => _currentBattleEnemy;
        public IReadOnlyReactiveProperty<Option<EnemyView>> PreBattleEnemy => _preBattleEnemy;
        public IReadOnlyReactiveProperty<List<Option<EnemyView>>> WaitingEnemyList => _waitingEnemyList;
        
        ReactiveProperty<Option<EnemyView>> _currentBattleEnemy;
        ReactiveProperty<Option<EnemyView>> _preBattleEnemy;
        ReactiveProperty<List<Option<EnemyView>>> _waitingEnemyList;

        int _currentWaitingEnemyCount;
        
        EnemyGeneratorMono _enemyGeneratorMono;
        EnemyObjectPool _enemyObjectPool;
        
        [Inject]
        public BattleEnemySwitcher(EnemyGeneratorMono enemyGeneratorMono,EnemyObjectPool enemyObjectPool)
        {
            _enemyGeneratorMono = enemyGeneratorMono;
            _enemyObjectPool = enemyObjectPool;
            
            _waitingEnemyList = new ReactiveProperty<List<Option<EnemyView>>>();

            _currentWaitingEnemyCount = 1;
            GenerateWaitingEnemy(2);
        }

        public void SwitchToNextEnemy()
        {
            _enemyObjectPool.AddDestroyedEnemy(_preBattleEnemy.Value);
            _preBattleEnemy = _currentBattleEnemy;
            // 1. 現在の戦闘敵を切り替え
            _currentBattleEnemy = new ReactiveProperty<Option<EnemyView>>(_waitingEnemyList.Value[0]); // インデックス 0 の敵を設定
            _waitingEnemyList.Value.RemoveAt(0); // リストから削除

            // 必要な数の敵を生成
            var generateCount = Mathf.Max(0, _currentWaitingEnemyCount - _waitingEnemyList.Value.Count);
            GenerateWaitingEnemy(generateCount);

            // 超過分の敵を削除(呼ばれることはないと思うけど)
            for (int i = _waitingEnemyList.Value.Count - 1; i >= _currentWaitingEnemyCount; i--)
            {
                Debug.LogError("何で呼ばれた？");
                _waitingEnemyList.Value.RemoveAt(i); // リストの末尾から削除
            }
        }

        public void ChangeWaitingEnemyCount(int enemyCount)
        {
            _currentWaitingEnemyCount = Mathf.Max(1,enemyCount);
        }

        void GenerateWaitingEnemy(int generateWaitingEnemyCount)
        {
            for (int i = 0; i < generateWaitingEnemyCount; i++)
            {
                var randomEnemyKind = DecideRandomEnemyKind();
                var newEnemy = _enemyGeneratorMono.GenerateEnemy(randomEnemyKind);
                _waitingEnemyList.Value.Add(newEnemy);
            }
        }

        EnemyKind DecideRandomEnemyKind()
        {
            var randomProbability = Random.Range(0, 100);
            const float babyAppearProbability = 70;
            
            if (randomProbability < babyAppearProbability)
                return EnemyKind.Baby;
            else
                return EnemyKind.Adult;
        }
    }
}