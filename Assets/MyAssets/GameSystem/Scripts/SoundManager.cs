#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class SoundManager:MonoBehaviour
    {
        [SerializeField] AudioSource bgmAudioSource;
        [SerializeField] AudioSource seAudioSource;

        [SerializeField] List<BGMSoundData> bgmSoundDatas;
        [SerializeField] List<SESoundData> seSoundDatas;

        private Dictionary<SESoundData.SE, AudioSource> loopingSEAudioSources = new();
        
        public float masterVolume = 1;
        public float bgmMasterVolume = 1;
        public float seMasterVolume = 1;

        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void PlayBGM(BGMSoundData.BGM bgm)
        {
            BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
            bgmAudioSource.clip = data.audioClip;
            bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
            bgmAudioSource.Play();
        }
        public void StopBGM(BGMSoundData.BGM bgm)
        {
            BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
            bgmAudioSource.clip = data.audioClip;
            bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
            bgmAudioSource.Stop();
        }

        public void PlaySEOneShot(SESoundData.SE se)
        {
            SESoundData data = seSoundDatas.Find(data => data.se == se);
            seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
            seAudioSource.PlayOneShot(data.audioClip);
        }

        public void PlayLoopingSE(SESoundData.SE se)
        {
            if (loopingSEAudioSources.ContainsKey(se)) return;

            SESoundData? data = seSoundDatas.Find(data => data.se == se);
            if (data == null) return;

            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = data.audioClip;
            newAudioSource.volume = data.volume * seMasterVolume * masterVolume;
            newAudioSource.loop = true;
            newAudioSource.Play();

            loopingSEAudioSources[se] = newAudioSource;
        }

        public void StopLoopingSE(SESoundData.SE se)
        {
            if (!loopingSEAudioSources.TryGetValue(se, out AudioSource? audioSource)) return;

            audioSource.Stop();
            Destroy(audioSource);
            loopingSEAudioSources.Remove(se);
        }

        public void StopAllLoopSE()
        {
            foreach (var audio in loopingSEAudioSources.Values)
            {
                audio.mute = true;
            }
        }
    }

    [System.Serializable]
    public class BGMSoundData
    {
        public enum BGM
        {
            PlayBgm,
            ZoneBgm
        }

        public BGM bgm;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume = 1;
    }

    [System.Serializable]
    public class SESoundData
    {
        public enum SE
        {
            CloseHandDefault,
            CloseHandInZone,
            OpenHandDefault,
            OpenHandInZone,
            EndClap,
            GoEndZone,
            GoHumanMode,
            GoInZone,
            GoMoneyMode,
            AdultBad,
            AdultExcellent,
            AdultGood,
            AdultMiss,
            BabyBad,
            BabyExcellent,
            BabyGood,
            BabyMiss
        }

        public SE se;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume = 1;
        }
}