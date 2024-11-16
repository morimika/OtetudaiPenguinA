using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //シーン遷移先
    [SerializeField,Header("シーン遷移先")] private bool _toPlay;
    //フェード管理スクリプト
    [SerializeField] private FadeAnimation _fadeAnimation;
    public static bool _isTap;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ボタン系を押さないときは全て処理しない
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Debug.Log("てすと");
            OnStartTap();
        }
    }


    public void OnStartTap()
    {
        Debug.Log("OnStartTap");
        
        _isTap = true;
        
        //フェードインが終わったら
        if (_fadeAnimation._fadeInEnd)
        {
           
            
        }
        
        //シーンを切り替える
        if (_toPlay)
        {
            Invoke("CallChangeScene",2f);
        }
    }
    /// <summary>
    /// シーン切り替えの関数
    /// </summary>
    private void CallChangeScene()
    {
        //メニュー中であればなにもしない
        //if(PanelManager._isPaused) return;
        //メニューを開いてなければシーンへ飛ぶ
        //if (!PanelManager._isPaused)
       
        SceneManager.LoadScene("Yuria_PlayScene");
        
       
    }
}
