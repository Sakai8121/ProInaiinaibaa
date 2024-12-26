#nullable enable
using MobileLibrary.Function;
using MyAssets.Enemy.Scripts;
using UnityEngine;
using VContainer;
using View;

namespace Model.Enemy
{
    public class EnemyGeneratorMono : MonoBehaviour
    {
        [SerializeField] Option<EnemyAdultViewMono> _enemyAdultViewMono;
        [SerializeField] Option<EnemyBabyViewMono> _enemyBabyViewMono;

        EnemyObjectPool _enemyObjectPool = null!;

        [Inject]
        public void Construct(EnemyObjectPool enemyObjectPool)
        {
            _enemyObjectPool = enemyObjectPool;
        }
        
        public Option<EnemyView> GenerateEnemy(EnemyKind enemyKind)
        {
            return enemyKind switch
            {
                EnemyKind.Adult => _enemyAdultViewMono.Match<Option<EnemyView>>(
                    None: () =>
                    {
                        Debug.LogError("No Adult Enemy prefab available.");
                        return Function.none;
                    },
                    Some: enemyPrefab => GenerateEnemyAdult(enemyPrefab)
                ),

                EnemyKind.Baby => _enemyBabyViewMono.Match<Option<EnemyView>>(
                    None: () =>
                    {
                        Debug.LogError("No Baby Enemy prefab available.");
                        return Function.none;
                    },
                    Some: enemyPrefab => GenerateEnemyBaby(enemyPrefab)
                ),
                _ => Function.none
            };
        }


        EnemyView InstantiateEnemyAdultView(EnemyAdultViewMono enemyAdultViewMono)
        {
            var enemyInstance = Instantiate(enemyAdultViewMono);
            return new EnemyView(enemyInstance, enemyInstance);
        }

        EnemyView InstantiateEnemyBabyView(EnemyBabyViewMono enemyBabyViewMono)
        {
            var enemyInstance = Instantiate(enemyBabyViewMono);
            return new EnemyView(enemyInstance, enemyInstance);
        }


        Option<EnemyView> GenerateEnemyAdult(EnemyAdultViewMono enemyAdultViewMono)
        {
            var enemy = _enemyObjectPool.GetEnemy();

            return enemy.Match<Option<EnemyView>>(
                None: () =>
                {
                    var enemyInstance = InstantiateEnemyAdultView(enemyAdultViewMono);
                    return new EnemyView(enemyInstance, enemyInstance);
                },
                Some: existingEnemy => existingEnemy);
        }

        Option<EnemyView> GenerateEnemyBaby(EnemyBabyViewMono enemyBabyViewMono)
        {
            var enemy = _enemyObjectPool.GetEnemy();

            return enemy.Match<Option<EnemyView>>(
                None: () =>
                {
                    var enemyInstance = InstantiateEnemyBabyView(enemyBabyViewMono);
                    return new EnemyView(enemyInstance, enemyInstance);
                },
                Some: existingEnemy => existingEnemy);
        }



    }
}