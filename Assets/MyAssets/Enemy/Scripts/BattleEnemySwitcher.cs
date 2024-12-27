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
        public Option<EnemyMono> CurrentBattleEnemy { get; private set; }
        public Option<EnemyMono> PreBattleEnemy { get; private set; }
        public List<Option<EnemyMono>> WaitingEnemyList { get; }

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
            _enemyObjectPool.AddDestroyedEnemy(PreBattleEnemy);
            PreBattleEnemy = CurrentBattleEnemy;
            // 1. 現在の戦闘敵を切り替え
            CurrentBattleEnemy = WaitingEnemyList[0]; // インデックス 0 の敵を設定
            WaitingEnemyList.RemoveAt(0); // リストから削除

            // 必要な数の敵を生成
            var generateCount = Mathf.Max(0, _currentWaitingEnemyCount - WaitingEnemyList.Count);
            Debug.LogError(generateCount);
            GenerateWaitingEnemy(generateCount);

            // 超過分の敵を削除(ゾーン状態が終わった後は元の数に戻る)
            for (int i = WaitingEnemyList.Count - 1; i >= _currentWaitingEnemyCount; i--)
            {
                WaitingEnemyList.RemoveAt(i); // リストの末尾から削除
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