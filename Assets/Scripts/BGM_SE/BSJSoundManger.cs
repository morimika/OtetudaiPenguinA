using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BSJSoundManger : MonoBehaviour
{
   
    
    [Header("BGMのリスト")]
    [SerializeField] private AudioClip[] _bgmClips;
    
    [Header("SEのリスト")]
    [SerializeField] private AudioClip[] _seClips;

    [Header("Jingleのリスト")]
    [SerializeField] private AudioClip[] _jingleClips;

    [Header("BGMのAudioSource")]
    [SerializeField] private AudioSource _bgmSorce;
    
    [Header("SEのAudioSource")]
    [SerializeField] private AudioSource _seSorce;
    
    [Header("JingleのAudioSource")]
    [SerializeField] private AudioSource _jingleSorce;
    
    //ジングル再生中はBGMを下げる（デフォルト音量の２０％に設定
    [Header("Jingleの再生中のBGM音量")]
    [SerializeField] private float _bgmVolumeDuringJingle = 0.2f;
    
    //元のBGM音量を保持
    private float _originalBGMVolume;
    
    //シングルトンとして管理
    public static BSJSoundManger Instance;

    private void Awake()
    {
        //起動時に生成されていなければ生成する
        if (Instance == null)
        {
            Instance = this;
            //シーンを跨いで保存
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            //既存のものは削除
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// BGMを再生関数
    /// </summary>
    /// <returns>bgmClipsのインデックス</returns>
    public void PlayBGM(int index)
    {
        //指定されたインデックスより少ない値や大きい値が指定された時はワーニングを出す
        if (index < 0 || index >= _bgmClips.Length)
        {
            Debug.LogWarning("指定されたBGMインデックスが無効だよ！！！！！" + index);
            return;
        }
        //配列の中身を参照
        _bgmSorce.clip = _bgmClips[index];
        //ループ再生を有効
        _bgmSorce.loop = true;
        //デフォルトの音量で再生
        _bgmSorce.volume = 1.0f;
        //再生
        _bgmSorce.Play();
    }

    /// <summary>
    /// BGMを停止
    /// </summary>
    public void StopBGM()
    {
        _bgmSorce.Stop();
    }

    /// <summary>
    /// SEを流す関数
    /// </summary>
    /// <param name="index"></param>
    
    private Dictionary<int,AudioSource> _activeSE = new Dictionary<int,AudioSource>();
    public async UniTask PlaySEAsync(int index)
    {
        //指定されたインデックスより少ない値や大きい値が指定された時はワーニングを出す
        if (index < 0 || index >= _seClips.Length)
        {
            Debug.LogWarning("指定されたSEインデックスが無効だよ！！！！！" + index);
            return;
        }
        
        //SEは一度だけ再生する
        //_seSorce.PlayOneShot(_seClips[index]);
        
        //すでに再生中ならSEをチェックして、同じインデックスの時は上書き
        if (_activeSE.ContainsKey(index))
        {
            //前のSEを停止
            _activeSE[index].Stop();
        }
        else
        {
             //新しいAUdioSorceを作成
            AudioSource _newSource = gameObject.AddComponent<AudioSource>();
            _newSource.playOnAwake = false;
            _activeSE[index] = _newSource;
        }
        
        //新しいSE再生
        AudioSource _source = _activeSE[index];
        _source.clip = _seClips[index];
        _source.Play();
        
        
        //SEの再生終了を非同期で待機
        await UniTask.WaitUntil(() =>  _source != null && !_source.isPlaying);
        
        _activeSE.Remove(index);
        
        // nullチェック後に削除
        if (_source != null)
        {
            Destroy(_source);
        }
        
        Debug.Log($"SE{index}の再生は終了");
       
        
        
    }


    public async UniTask PlayJingleAsync(int index)
    {
        //指定されたインデックスより少ない値や大きい値が指定された時はワーニングを出す
        if (index < 0 || index >= _jingleClips.Length)
        {
            Debug.LogWarning("指定されたJingleインデックスが無効だよ！！！！！" + index);
            return;
        }
        
        //現在のBGM音量を保存
        _originalBGMVolume = _bgmSorce.volume;
        
        //ジングル再生中はボリュームを下げる
        _bgmSorce.volume =  _bgmVolumeDuringJingle; 
        
        //配列の中身を参照
        _jingleSorce.clip = _jingleClips[index];
        //ループ再生は無効
        _jingleSorce.loop = false;
        //再生
        _jingleSorce.Play();

        //ジングルの再生が終わるまで待機
        await UniTask.WaitUntil(() => !_jingleSorce.isPlaying);
        
        //ジングル終了後にBGM音量を元に戻す
        _bgmSorce.volume = _originalBGMVolume;
    }
    


    //全てのサウンドを停止する関数
    public void StopAllSounds()
    {
        _bgmSorce.Stop();
        _seSorce.Stop();
        _jingleSorce.Stop();
    }
    
}
