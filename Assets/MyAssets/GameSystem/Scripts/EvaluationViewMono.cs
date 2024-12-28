#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationViewMono:MonoBehaviour
    {
        [SerializeField] Image evaluationGaugeImage = null!;
        [SerializeField] Sprite defaultGaugeSprite = null!;
        [SerializeField] Sprite inZoneGaugeSprite = null!;
        [SerializeField] Image missSpriteImage = null!;

        public void ChangeGaugeToDefault()
        {
            evaluationGaugeImage.sprite = defaultGaugeSprite;
        }
        
        public void ChangeGaugeToInZone()
        {
            evaluationGaugeImage.sprite = inZoneGaugeSprite;
        }
        
        public void ChangeGaugeView(float gaugeFillAmount)
        {
            if (gaugeFillAmount >= 1 && evaluationGaugeImage.sprite != inZoneGaugeSprite)
                missSpriteImage.enabled = true;
            else
                missSpriteImage.enabled = false;
            
            evaluationGaugeImage.fillAmount = gaugeFillAmount;
        }
    }
}