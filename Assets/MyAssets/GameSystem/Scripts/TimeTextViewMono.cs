#nullable enable
using TMPro;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class TimeTextViewMono:MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText = null!;

        public void ChangeTimeText(int time)
        {
            timeText.text = time.ToString();
        }
    }
}