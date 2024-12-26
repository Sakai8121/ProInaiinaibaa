#nullable enable
using System.Collections.Generic;
using MobileLibrary.Function;
using UnityEngine;
using VContainer;

namespace MyAssets.Enemy.Scripts
{
    public class EnemyObjectPool
    {
        List<Option<EnemyResult>>? _enemyViewList;

        public void AddDestroyedEnemy(Option<EnemyResult> enemyView)
        {
            if (_enemyViewList == null)
                _enemyViewList = new List<Option<EnemyResult>>();
            
            _enemyViewList.Add(enemyView);
        }
        
        public Option<EnemyResult> GetEnemy()
        {
            if (_enemyViewList == null)
                _enemyViewList = new List<Option<EnemyResult>>();

            if (_enemyViewList.Count != 0)
            {
                var enemy = _enemyViewList[0];
                _enemyViewList.Remove(enemy);
                return enemy;
            }
            else
            {
                return Function.none;
            }
        }
    }
}