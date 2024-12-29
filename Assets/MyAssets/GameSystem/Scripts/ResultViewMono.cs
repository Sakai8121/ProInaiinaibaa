#nullable enable
using System.Collections.Generic;
using General;
using Model.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using unityroom.Api;
using VContainer;

namespace MyAssets.GameSystem.Scripts
{
    public class ResultViewMono:MonoBehaviour
    {
        [SerializeField] GameObject resultParentObject = null!;
        [SerializeField] GameObject playUIParentObject = null!;
        [SerializeField] TextMeshProUGUI scoreText = null!;
        [SerializeField] TextMeshProUGUI excellentText = null!;
        [SerializeField] TextMeshProUGUI goodText = null!;
        [SerializeField] TextMeshProUGUI badText = null!;
        [SerializeField] TextMeshProUGUI missText = null!;
        [SerializeField] TextMeshProUGUI comboText = null!;

        [SerializeField] Button endGameButton = null!;
        [SerializeField] Button retryGameButton = null!;
        [SerializeField] Button backTitleButton = null!;

        [Inject]
        public void Construct(SoundManager soundManager,DisposeManager disposeManager)
        {
            endGameButton.onClick.AddListener(() =>
            {
                soundManager.DeleteInstance();
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                #else
                    Application.Quit();//ゲームプレイ終了
                #endif
            });
            retryGameButton.onClick.AddListener(()=>
            {
                soundManager.DeleteInstance();
                disposeManager.Dispose();
                SceneManager.LoadScene("PlayScene");
            });
            backTitleButton.onClick.AddListener(()=>
            {
                soundManager.DeleteInstance();
                disposeManager.Dispose();
                SceneManager.LoadScene("TitleScene");
            });
        }

        public void ActiveResultUI(int score,int maxCombo,Dictionary<EvaluationData.Evaluation,int> evaluationCountDictionary)
        {
            resultParentObject.SetActive(true);
            playUIParentObject.SetActive(false);
            
            scoreText.text = $"{score:N0}";
            excellentText.text = evaluationCountDictionary[EvaluationData.Evaluation.Excellent].ToString();
            goodText.text = evaluationCountDictionary[EvaluationData.Evaluation.Good].ToString();
            badText.text = evaluationCountDictionary[EvaluationData.Evaluation.Normal].ToString();
            missText.text = evaluationCountDictionary[EvaluationData.Evaluation.Miss].ToString();

            comboText.text = maxCombo.ToString();
            
            // ボードNo1にスコア123.45fを送信する。
            UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);

        }
        
    }
}