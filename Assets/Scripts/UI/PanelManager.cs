
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    //ポーズ中かどうかの判定
    public static bool _isPaused;
    //プレイヤーの動きのスクリプト
    //[SerializeField] private PlayerTapMove _playerTapMove;
    [SerializeField] private PlaySceneDatas _playSceneDatas;
    private Button _resumeButton;
    //表示するパネル先
    [SerializeField] private GameObject _pauseMenuUI;


    void Start()
    {
        _resumeButton = GetComponent<Button>();
    }

    void Update()
    {

    }

    public void OnTap()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
            
        }
    }

    /// <summary>
    /// ゲームに戻る処理
    /// </summary>
    public void Resume()
    {
        
        gameObject.SetActive(false);
        _playSceneDatas.TapType = PlaySceneTapType.Player;
        //パネルを消す
        _pauseMenuUI.SetActive(false);
        //StartCoroutine("PauseEach");

    }
    
/*
    IEnumerator PauseEach()
    {
        
        //待機してから次の処理へ
        yield return new WaitForSeconds(0.5f);

        _isPaused = false;
    }
    */

    public void Pause()
    {
        _isPaused = true;
        //パネルの表示
        _pauseMenuUI.SetActive(true);
        
        if (_playSceneDatas.TapType.HasFlag(PlaySceneTapType.Player))
        {
            _playSceneDatas.TapType = PlaySceneTapType.Panel;
            _pauseMenuUI ? .SetActive(true);
        }
        
        
    }
    
}
