
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    //ポーズ中かどうかの判定
    public static bool _isPaused;
    //プレイヤーの動きのスクリプト
    [SerializeField] private PlayerTapMove _playerTapMove;
    //表示するパネル先
    public GameObject _pauseMenuUI;
    
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
        //操作受け付ける
        _playerTapMove.enabled = true;
        
        //パネルを消す
        _pauseMenuUI.SetActive(false);
        //ゲーム内の時間を等速にする
        Time.timeScale = 1f;
        _isPaused = false;
        
    }

    public void Pause()
    {
        //プレイヤーの操作を受け付けない
        _playerTapMove.enabled = false;
        //パネルの表示
        _pauseMenuUI.SetActive(true);
        //ゲーム内の時間を止める
        Time.timeScale = 0f;
        _isPaused = true;
        
    }
}
