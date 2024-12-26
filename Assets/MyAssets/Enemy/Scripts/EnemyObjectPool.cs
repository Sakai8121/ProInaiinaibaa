#nullable enable
using System.Collections.Generic;
using MobileLibrary.Function;
using UnityEngine;
using VContainer;

namespace MyAssets.Enemy.Scripts
{
    public class EnemyObjectPool
    {
        List<Option<EnemyView>>? _enemyViewList;

        public void AddDestroyedEnemy(Option<EnemyView> enemyView)
        {
            if (_enemyViewList == null)
                _enemyViewList = new List<Option<EnemyView>>();
            
            _enemyViewList.Add(enemyView);
        }
        
        public Option<EnemyView> GetEnemy()
        {
            if (_enemyViewList == null)
                _enemyViewList = new List<Option<EnemyView>>();

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