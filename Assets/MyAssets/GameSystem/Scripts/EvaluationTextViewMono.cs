#nullable enable
using Model.GameSystem;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class EvaluationTextViewMono:MonoBehaviour
    {
        [SerializeField] GameObject excellentTextObject = null!;
        [SerializeField] GameObject goodTextObject = null!;
        [SerializeField] GameObject badTextObject = null!;
        [SerializeField] GameObject missTextObject = null!;

        public void ActiveEvaluationText(EvaluationData.Evaluation evaluation)
        {
            DisActiveAllText();
            
            switch (evaluation)
            {
                case EvaluationData.Evaluation.Normal:
                    badTextObject.SetActive(true);
                    break;
                case EvaluationData.Evaluation.Good:
                    goodTextObject.SetActive(true);
                    break;
                case EvaluationData.Evaluation.Excellent:
                    excellentTextObject.SetActive(true);
                    break;
                case EvaluationData.Evaluation.Miss:
                    missTextObject.SetActive(true);
                    break;
            }
        }

        void DisActiveAllText()
        {
            badTextObject.SetActive(false);
            goodTextObject.SetActive(false);
            excellentTextObject.SetActive(false);
            missTextObject.SetActive(false);
        }
    }
}