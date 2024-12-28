#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationViewMono:MonoBehaviour
    {
        [SerializeField] Image evaluationGaugeImage = null!;
        
        public void ChangeGaugeView(float gaugeFillAmount)
        {
            evaluationGaugeImage.fillAmount = gaugeFillAmount;
        }
    }
}