#nullable enable
using Model.GameSystem;
using UnityEngine;
using View;

namespace MyAssets.Enemy.Scripts
{
    public class EnemyMono:MonoBehaviour
    {
        public AbstractEnemyViewMono EnemyViewMono => enemyViewMono;
        [SerializeField] AbstractEnemyViewMono enemyViewMono;
        
    }
}