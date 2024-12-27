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
        [SerializeField] OptionMono<EnemyMono> enemyAdultMono;
        [SerializeField] OptionMono<EnemyMono> enemyBabyMono;

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
            var enemy = _enemyObjectPool.GetEnemy();

            return enemy
                .Where(existingEnemy  => existingEnemy .EnemyKind == enemyKind)
                .Match<Option<EnemyMono>>(
                None: () =>
                {
                    var enemyInstance = Instantiate(enemyMono);
                    return enemyInstance;
                },
                Some: existingEnemy => existingEnemy);
        }



    }
}