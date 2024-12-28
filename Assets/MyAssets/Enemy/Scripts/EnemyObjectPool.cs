#nullable enable
using System.Collections.Generic;
using System.Linq;
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
        
        public Option<EnemyMono> GetEnemy(EnemyKind enemyKind)
        {
            if (_enemyMonoList == null)
                _enemyMonoList = new List<Option<EnemyMono>>();

            var matchingEnemy = _enemyMonoList
                .Where<Option<EnemyMono>>(enemyOption => enemyOption.Match(
                    None: () => false,
                    Some: enemy => enemy.EnemyKind == enemyKind
                ))
                .ToList();
            
            if (matchingEnemy.Count != 0)
            {
                var option = matchingEnemy[0];
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