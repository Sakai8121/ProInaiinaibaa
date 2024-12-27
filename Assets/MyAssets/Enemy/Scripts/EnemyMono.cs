#nullable enable
using Model.GameSystem;
using UnityEngine;
using View;

namespace MyAssets.Enemy.Scripts
{
    public class EnemyMono:MonoBehaviour
    {
        public EnemyKind EnemyKind { get; set; }
        public AbstractEnemyViewMono EnemyViewMono => enemyViewMono;
        [SerializeField] AbstractEnemyViewMono enemyViewMono;

        public void Init(EnemyKind enemyKind)
        {
            EnemyKind = enemyKind;
            EnemyViewMono.ChangePosition(EnemyDefaultParameter.GeneratePosition,false);
            EnemyViewMono.InitSprite();
            gameObject.SetActive(true);
        }
    }
}