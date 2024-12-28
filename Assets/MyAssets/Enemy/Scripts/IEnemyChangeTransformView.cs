using DG.Tweening;
using UnityEngine;

namespace MyAssets.Enemy.Scripts
{
    public interface IEnemyChangeTransformView
    {
        void ChangePosition(Vector2 position,bool requiresAnimation);
        Sequence Blown();
    }
}