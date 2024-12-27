#nullable enable
using System.Collections.Generic;
using MobileLibrary.Function;
using UnityEngine;
using VContainer;

namespace MyAssets.Enemy.Scripts
{
    public class EnemyObjectPool
    {
        List<Option<EnemyMono>>? _enemyMonoList;

        public void AddDestroyedEnemy(Option<EnemyMono> enemyView)
        {
            if (_enemyMonoList == null)
                _enemyMonoList = new List<Option<EnemyMono>>();
            
            _enemyMonoList.Add(enemyView);
        }
        
        public Option<EnemyMono> GetEnemy()
        {
            if (_enemyMonoList == null)
                _enemyMonoList = new List<Option<EnemyMono>>();

            if (_enemyMonoList.Count != 0)
            {
                var enemy = _enemyMonoList[0];
                _enemyMonoList.Remove(enemy);
                return enemy;
            }
            else
            {
                return Function.none;
            }
        }
    }
}