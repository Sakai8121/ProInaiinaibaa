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
        public Option<EnemyMono> CurrentBattleEnemy => _currentBattleEnemy;
        public Option<EnemyMono> PreBattleEnemy => _preBattleEnemy;
        public List<Option<EnemyMono>> WaitingEnemyList => _waitingEnemyList;
        
        Option<EnemyMono> _currentBattleEnemy;
        Option<EnemyMono> _preBattleEnemy;
        List<Option<EnemyMono>> _waitingEnemyList;

        int _currentWaitingEnemyCount;
        
        EnemyGeneratorMono _enemyGeneratorMono;
        EnemyObjectPool _enemyObjectPool;
        
        [Inject]
        public BattleEnemySwitcher(EnemyGeneratorMono enemyGeneratorMono,EnemyObjectPool enemyObjectPool)
        {
            _enemyGeneratorMono = enemyGeneratorMono;
            _enemyObjectPool = enemyObjectPool;
            
            _waitingEnemyList = new List<Option<EnemyMono>>();

            _currentWaitingEnemyCount = 1;
            GenerateWaitingEnemy(2);
        }

        public void SwitchToNextEnemy()
        {
            _enemyObjectPool.AddDestroyedEnemy(_preBattleEnemy);
            _preBattleEnemy = _currentBattleEnemy;
            // 1. 現在の戦闘敵を切り替え
            _currentBattleEnemy = _waitingEnemyList[0]; // インデックス 0 の敵を設定
            _waitingEnemyList.RemoveAt(0); // リストから削除

            // 必要な数の敵を生成
            var generateCount = Mathf.Max(0, _currentWaitingEnemyCount - _waitingEnemyList.Count);
            GenerateWaitingEnemy(generateCount);

            // 超過分の敵を削除(ゾーン状態が終わった後は元の数に戻る)
            for (int i = _waitingEnemyList.Count - 1; i >= _currentWaitingEnemyCount; i--)
            {
                _waitingEnemyList.RemoveAt(i); // リストの末尾から削除
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
                _waitingEnemyList.Add(newEnemy);
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