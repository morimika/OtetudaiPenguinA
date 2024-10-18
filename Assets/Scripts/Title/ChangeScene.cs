using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //シーン遷移先
    [SerializeField,Header("シーン遷移先")] private bool _toPlay;
    //フェード管理スクリプト
    [SerializeField] private FadeAnimation _fadeAnimation;
    public static bool _isTap;

   

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void OnStartTap()
    {
        _isTap = true;
        //フェードインが終わったら
        if (_fadeAnimation._fadeInEnd)
        {
            Debug.Log("call2222");
            
        }
        
        //シーンを切り替える
        if (_toPlay)
        {
            Invoke("CallChangeScene",3f);
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
        
        Debug.Log("呼んだ");
        SceneManager.LoadScene("Yuria_PlayScene");
        
       
    }
}
