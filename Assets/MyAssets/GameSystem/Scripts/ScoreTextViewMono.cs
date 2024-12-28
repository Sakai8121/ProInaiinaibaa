#nullable enable
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class ScoreTextViewMono:MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText = null!;
        
        public void ChangeScoreText(int score)
        {
            scoreText.text = $"{score:N0}";
        }
    }
}