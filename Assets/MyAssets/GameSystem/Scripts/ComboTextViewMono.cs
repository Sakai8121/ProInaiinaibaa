#nullable enable
using TMPro;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class ComboTextViewMono:MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI comboText = null!;
        
        public void ChangeComboText(int comboCount)
        {
            comboText.text = comboCount.ToString();
        }
    }
}