using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //サウンド生成
    public static SoundManager instance;

    [SerializeField,Header("サウンドマネージャーのオブジェクト先")] AudioSource _bgmAudioSource;
    

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            //音量をいじっても次のシーンでも継続させる
            DontDestroyOnLoad(gameObject);
        } else
        {
            //nullじゃなければ消す
            Destroy(gameObject);
        }
    }

    //関数呼び出し用
    public void SetBGMVolume(float volume)
    {
       _bgmAudioSource.volume = volume;
    }
}
