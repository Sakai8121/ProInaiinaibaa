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
            switch (enemyKind)
            {
                case EnemyKind.Adult:
                    return _enemyAdultViewMono.Match<Option<EnemyView>>(
                        None: () =>
                        {
                            Debug.LogError("No Adult Enemy prefab available.");
                            return Function.none; // Return the initial None result
                        },
                        Some: enemyPrefab =>
                        {
                            var enemy = _enemyObjectPool.GetEnemy();

                            return enemy.Match<Option<EnemyView>>(
                                None: () =>
                                {
                                    var enemyInstance = Instantiate(enemyPrefab);
                                    return new EnemyView(enemyInstance, enemyInstance);
                                },
                                Some: existingEnemy =>
                                {
                                    return Function.Some(existingEnemy);
                                });
                        });

                case EnemyKind.Baby:
                    return _enemyBabyViewMono.Match<Option<EnemyView>>(
                        None: () =>
                        {
                            Debug.LogError("No Adult Enemy prefab available.");
                            return Function.none; // Return the initial None result
                        },
                        Some: enemyPrefab =>
                        {
                            var enemy = _enemyObjectPool.GetEnemy();

                            return enemy.Match<Option<EnemyView>>(
                                None: () =>
                                {
                                    var enemyInstance = Instantiate(enemyPrefab);
                                    return new EnemyView(enemyInstance, enemyInstance);
                                },
                                Some: existingEnemy =>
                                {
                                    return Function.Some(existingEnemy);
                                });
                        });

                default:
                    Debug.LogError($"Unhandled EnemyKind: {enemyKind}");
                    return Function.none;
            }
        }
        
    }
}