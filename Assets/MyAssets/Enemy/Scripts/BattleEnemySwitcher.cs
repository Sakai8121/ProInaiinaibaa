#nullable enable
using System.Collections.Generic;
using System.Linq;
using MobileLibrary.Function;
using MyAssets.Enemy.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace Model.Enemy
{
    public class BattleEnemySwitcher
    {
        public Option<EnemyMono> CurrentBattleEnemy { get; private set; }
        public Option<EnemyMono> DefeatedEnemy { get; private set; }
        public List<Option<EnemyMono>> WaitingEnemyList { get; private set; }

        int _currentWaitingEnemyCount;

        readonly EnemyGeneratorMono _enemyGeneratorMono;
        readonly EnemyObjectPool _enemyObjectPool;
        
        [Inject]
        public BattleEnemySwitcher(EnemyGeneratorMono enemyGeneratorMono,EnemyObjectPool enemyObjectPool)
        {
            _enemyGeneratorMono = enemyGeneratorMono;
            _enemyObjectPool = enemyObjectPool;
            
            WaitingEnemyList = new List<Option<EnemyMono>>();

            _currentWaitingEnemyCount = 1;
            GenerateWaitingEnemy(2);
            SwitchToNextEnemy();
        }

        public void SwitchToNextEnemy()
        {
            _enemyObjectPool.AddDestroyedEnemy(DefeatedEnemy);
            DefeatedEnemy = CurrentBattleEnemy;
            // 1. 現在の戦闘敵を切り替え
            CurrentBattleEnemy = WaitingEnemyList.First();
            WaitingEnemyList.RemoveAt(0);

            // 必要な数の敵を生成
            var generateCount = Mathf.Max(0, _currentWaitingEnemyCount - WaitingEnemyList.Count);
            Debug.LogError(generateCount);
            GenerateWaitingEnemy(generateCount);

            // 超過分の敵を削除(ゾーン状態が終わった後は元の数に戻る)
            WaitingEnemyList = WaitingEnemyList.Take(_currentWaitingEnemyCount).ToList();
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
                newEnemy.Do(ememy =>
                {
                    ememy.gameObject.SetActive(true);
                    ememy.EnemyViewMono.InitSprite();
                });
                WaitingEnemyList.Add(newEnemy);
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