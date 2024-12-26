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
        [SerializeField] Option<AbstractEnemyViewMono> _enemyAdultViewMono;
        [SerializeField] Option<AbstractEnemyViewMono> _enemyBabyViewMono;

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
                EnemyKind.Adult => _enemyAdultViewMono.Match(
                    None: () =>
                    {
                        Debug.LogError("No Adult Enemy prefab available.");
                        return Function.none;
                    },
                    Some: GenerateEnemy 
                ),

                EnemyKind.Baby => _enemyBabyViewMono.Match(
                    None: () =>
                    {
                        Debug.LogError("No Baby Enemy prefab available.");
                        return Function.none;
                    },
                    Some: GenerateEnemy 
                ),
                _ => Function.none
            };
        }


        EnemyView InstantiateEnemyAdultView(AbstractEnemyViewMono enemyAdultViewMono)
        {
            var enemyInstance = Instantiate(enemyAdultViewMono);
            return new EnemyView(enemyInstance, enemyInstance);
        }


        Option<EnemyView> GenerateEnemy(AbstractEnemyViewMono enemyAdultViewMono)
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



    }
}