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

        public Option<EnemyResult> GenerateEnemy(EnemyResult.EnemyKind enemyKind)
        {
            switch (enemyKind)
            {
                case EnemyResult.EnemyKind.Adult:
                    return _enemyAdultViewMono.Match<Option<EnemyResult>>(
                        None: () =>
                        {
                            Debug.LogError("No Adult Enemy prefab available.");
                            return Function.none; // Return the initial None result
                        },
                        Some: enemyPrefab =>
                        {
                            var enemy = _enemyObjectPool.GetEnemy();

                            return enemy.Match<Option<EnemyResult>>(
                                None: () =>
                                {
                                    var enemyInstance = Instantiate(enemyPrefab);
                                    return new EnemyResult(enemyInstance, enemyInstance);
                                },
                                Some: existingEnemy =>
                                {
                                    return Function.Some(existingEnemy);
                                });
                        });

                case EnemyResult.EnemyKind.Baby:
                    return _enemyBabyViewMono.Match<Option<EnemyResult>>(
                        None: () =>
                        {
                            Debug.LogError("No Adult Enemy prefab available.");
                            return Function.none; // Return the initial None result
                        },
                        Some: enemyPrefab =>
                        {
                            var enemy = _enemyObjectPool.GetEnemy();

                            return enemy.Match<Option<EnemyResult>>(
                                None: () =>
                                {
                                    var enemyInstance = Instantiate(enemyPrefab);
                                    return new EnemyResult(enemyInstance, enemyInstance);
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