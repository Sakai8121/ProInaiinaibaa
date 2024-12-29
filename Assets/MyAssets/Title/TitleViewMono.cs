#nullable enable
using System;
using MyAssets.GameSystem.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyAssets.Title
{
    public class TitleViewMono:MonoBehaviour
    {
        [SerializeField] Slider volumeSlider = null!;
        [SerializeField] Button noneTutorialButton = null!;
        [SerializeField] Button tutorialButton = null!;
        [SerializeField] AudioSource titleAudioSource = null!;

        void Awake()
        {
            volumeSlider.value = GameInfoHolderStatic.MasterVolume;
            titleAudioSource.volume = GameInfoHolderStatic.MasterVolume;
            volumeSlider.onValueChanged.AddListener(value =>
            {
                GameInfoHolderStatic.MasterVolume = value;
                titleAudioSource.volume = value;
            });
            noneTutorialButton.onClick.AddListener((() =>
            {
                GameInfoHolderStatic.IsRequiredTutorial = false;
                SceneManager.LoadScene("PlayScene");
            }));
            tutorialButton.onClick.AddListener((() =>
            {
                GameInfoHolderStatic.IsRequiredTutorial = true;
                SceneManager.LoadScene("PlayScene");
            }));
        }
    }
}