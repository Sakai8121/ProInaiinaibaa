#nullable enable
using System.Collections.Generic;
using MobileLibrary.Function;
using MyAssets.Enemy.Scripts;
using UnityEngine;
using VContainer;

namespace Model.Enemy
{
    public class BattleEnemySwitcher
    {
        public Option<EnemyResult> CurrentBattleEnemy => _currentBattleEnemy;
        public List<Option<EnemyResult>> WaitingEnemyList => _waitingEnemyList;
        
        Option<EnemyResult> _currentBattleEnemy;
        List<Option<EnemyResult>> _waitingEnemyList;

        int _currentWaitingEnemyCount;
        
        EnemyGeneratorMono _enemyGeneratorMono;
        
        [Inject]
        public BattleEnemySwitcher(EnemyGeneratorMono enemyGeneratorMono)
        {
            _enemyGeneratorMono = enemyGeneratorMono;
            _waitingEnemyList = new List<Option<EnemyResult>>();

            _currentWaitingEnemyCount = 1;
            GenerateWaitingEnemy(2);
        }

        public void SwitchToNextEnemy()
        {
            // 1. 現在の戦闘敵を切り替え
            _currentBattleEnemy = _waitingEnemyList[0]; // インデックス 0 の敵を設定
            _waitingEnemyList.RemoveAt(0); // リストから削除

            // 必要な数の敵を生成
            var generateCount = Mathf.Max(0, _currentWaitingEnemyCount - _waitingEnemyList.Count);
            GenerateWaitingEnemy(generateCount);

            // 超過分の敵を削除(呼ばれることはないと思うけど)
            for (int i = _waitingEnemyList.Count - 1; i >= _currentWaitingEnemyCount; i--)
            {
                Debug.LogError("何で呼ばれた？");
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

        EnemyResult.EnemyKind DecideRandomEnemyKind()
        {
            var randomProbability = Random.Range(0, 100);
            const float babyAppearProbability = 70;
            
            if (randomProbability < babyAppearProbability)
                return EnemyResult.EnemyKind.Baby;
            else
                return EnemyResult.EnemyKind.Adult;
        }
    }
}