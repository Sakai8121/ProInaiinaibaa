#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.GameSystem.Scripts
{
    public class WaitingStartImageViewMono:MonoBehaviour
    {
        [SerializeField] GameObject waitingStartImage = null!;
        [SerializeField] GameObject playUI = null!;
        
        public void DisActiveStartImage()
        {
            playUI.SetActive(true);
            waitingStartImage.SetActive(false);
        }
    }
}