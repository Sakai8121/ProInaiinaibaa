#nullable enable
using MobileLibrary.Function;
using MyAssets.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using View;

namespace Model.Enemy
{
    public class EnemyGeneratorMono : MonoBehaviour
    {
        [SerializeField] OptionMono<EnemyMono> enemyAdultMono = null!;
        [SerializeField] OptionMono<EnemyMono> enemyBabyMono = null!;
        [SerializeField] Transform enemyParent = null!;

        EnemyObjectPool _enemyObjectPool = null!;

        [Inject]
        public void Construct(EnemyObjectPool enemyObjectPool)
        {
            _enemyObjectPool = enemyObjectPool;
        }
        
        public Option<EnemyMono> GenerateEnemy(EnemyKind enemyKind)
        {
            return enemyKind switch
            {
                EnemyKind.Adult => enemyAdultMono.Match(
                    None: () =>
                    {
                        Debug.LogError("No Adult Enemy prefab available.");
                        return Function.none;
                    },
                    Some: prefab => GenerateEnemy(prefab, EnemyKind.Adult)
                ),

                EnemyKind.Baby => enemyBabyMono.Match(
                    None: () =>
                    {
                        Debug.LogError("No Baby Enemy prefab available.");
                        return Function.none;
                    },
                    Some: prefab => GenerateEnemy(prefab, EnemyKind.Baby)
                ),
                _ => Function.none
            };
        }

        Option<EnemyMono> GenerateEnemy(EnemyMono enemyMono,EnemyKind enemyKind)
        {
            var enemy = _enemyObjectPool.GetEnemy(enemyKind);

            return enemy
                .Match<Option<EnemyMono>>(
                None: () =>
                {
                    Debug.LogError("None");
                    var enemyInstance = Instantiate(enemyMono,enemyParent);
                    return enemyInstance;
                },
                Some: existingEnemy => existingEnemy);
        }
    }
}