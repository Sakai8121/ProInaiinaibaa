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

        public void AddDestroyedEnemy(Option<EnemyMono> option)
        {
            if (_enemyMonoList == null)
                _enemyMonoList = new List<Option<EnemyMono>>();
            
            option.Do(enemy=>
            {
                enemy.gameObject.SetActive(false);
                _enemyMonoList.Add(option);
            });
            
        }
        
        public Option<EnemyMono> GetEnemy()
        {
            if (_enemyMonoList == null)
                _enemyMonoList = new List<Option<EnemyMono>>();

            if (_enemyMonoList.Count != 0)
            {
                var option = _enemyMonoList[0];
                _enemyMonoList.Remove(option);
                return option;
            }
            else
            {
                return Function.none;
            }
        }
    }
}