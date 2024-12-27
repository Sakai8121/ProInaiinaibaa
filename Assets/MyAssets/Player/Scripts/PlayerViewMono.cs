#nullable enable
using Model.Player;
using UnityEngine;

namespace MyAssets.Player.Scripts
{
    public class PlayerViewMono:MonoBehaviour
    {
        [SerializeField] GameObject defaultSpriteObject = null!;
        [SerializeField] GameObject godSpriteObject = null!;

        public void ChangeSprite(PlayerStateHolder.PlayerState playerState)
        {
            //スプライトがイラスト屋でスケールが違うからオブジェクトをアクティブ非アクティブする方法にしている
            DisActiveAllSprite();
            
            switch (playerState)
            {
                case PlayerStateHolder.PlayerState.Default:
                    defaultSpriteObject.SetActive(true);
                    break;
                case PlayerStateHolder.PlayerState.God:
                    godSpriteObject.SetActive(true);
                    break;
            }
        }

        void DisActiveAllSprite()
        {
            defaultSpriteObject.SetActive(false);
            godSpriteObject.SetActive(false);
        }
    }
}