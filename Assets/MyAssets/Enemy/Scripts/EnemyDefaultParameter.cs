#nullable enable
using UnityEngine;

namespace MyAssets.Enemy.Scripts
{
    public static class EnemyDefaultParameter
    {
        public static readonly Vector2 GeneratePosition = new Vector2(12, -3);
        
        public static readonly float MoveAnimationSpeed = 0.5f;
        
        public static readonly  float BlownAnimationSpeed = 2f;
        public static readonly Vector3 RotateValue = new Vector3(0,0,350);
        public static readonly Vector2 BlownPosition = new Vector2(10, 5);
    }
}