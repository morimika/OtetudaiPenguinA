using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2 : MonoBehaviour
{
    //オーディオソースを取得する場所
    [SerializeField,Header("BGMのAudioSourceを入れる場所")] AudioSource _bgmAudioSource;
    [SerializeField,Header("SEのAudioSourceを入れる場所")] AudioSource _seAudioSource;
    //サウンドのデータを取得する場所
    [SerializeField,Header("BGMデータを入れる場所")] List<BGMSoundData> _bgmSoundDatas;
    [SerializeField,Header("SEデータを入れる場所")] List<SESoundData> _seSoundDatas;
    
    //ボリューム系の初期値
    public float _masterVolume = 1;
    public float _bgmMasterVolume = 1;
    public float _seMasterVolume = 1;

    public static SoundManager2 Instance{get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// BGMを流す関数
    /// </summary>
    /// <param name="bgm"></param>

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = _bgmSoundDatas.Find(data => data.bgm == bgm);
        _bgmAudioSource.clip = data.audioClip;
        _bgmAudioSource.volume = data.volume * _bgmMasterVolume * _masterVolume;
        _bgmAudioSource.Play();
    }

    /// <summary>
    /// SEを流すSE
    /// </summary>
    /// <param name="se"></param>
    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = _seSoundDatas.Find(data => data.se == se);
        _seAudioSource.volume = data.volume * _seMasterVolume * _masterVolume;
        _seAudioSource.PlayOneShot(data.audioClip);
    }

}
/// <summary>
///何のBGMかラベル設定
/// </summary>

    [System.Serializable]
    public class BGMSoundData
    {
        public enum BGM
        {
            Title,
            Map,
            StartHelp,
        }

        public BGM bgm;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume = 1;
    }
/// <summary>
/// 何のSEかのラベル設定
/// </summary>
    [System.Serializable]
    public class SESoundData
    {
        public enum SE
        {
            Click,
            GameClear,
            GameOver, 
            Load,
        }

        public SE se;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume = 1;
    }
