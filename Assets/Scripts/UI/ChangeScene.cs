using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeScene : SingletonMonoBehaviour<ChangeScene>
{
    //シーン遷移先
    [SerializeField,Header("シーン遷移先")] private bool _toPlay;
    //フェード管理スクリプト
    [SerializeField] private FadeAnimation _fadeAnimation;
   
    public                   FadeAnimation FadeAnim => _fadeAnimation;


    public void LoadNextScene(int sceneIndex)
    {
        StartCoroutine(NextScene(sceneIndex));
    }
    
    /// <summary>
    /// 次のシーン呼び出し
    /// </summary>
    /// <param name="sceneIndex">シーン番号</param>
    /// <returns></returns>
    public IEnumerator NextScene(int sceneIndex)
    {
        //  フェーアウト（画面内にパネルが入ってくる）
        yield return StartCoroutine(_fadeAnimation.FadeOutASync());
        //  フェードアウトが終了したのを検出する
        yield return new WaitUntil(() => _fadeAnimation.IsFadeEnd);
        //  シーン呼び出しを行う
        SceneManager.LoadSceneAsync(sceneIndex);
        
        yield return new WaitForSeconds(1);
        
        Debug.Log("call fade in");
        //  フェードイン（画面内のパネルが外に出る）
        yield return StartCoroutine(_fadeAnimation.FadeInASync());
        //  フェードアウトが終了したのを検出する
        yield return new WaitUntil(() => _fadeAnimation.IsFadeEnd);
    }
    
    /// <summary>
    /// マウスクリック（全画面での）検出
    /// </summary>
    /// <param name="mouseType">0:左、1:右、2:中央</param>
    /// <param name="clickMode">下のオブジェクトを無視する場合 true</param>
    /// <returns></returns>
    public bool IsMouseClicked(int mouseType, bool clickMode)
    {
        if (Input.GetMouseButtonDown(mouseType))
        {
            
            //ボタン系を押さないときは全て処理しない
            if (clickMode)
            {
                if (EventSystem.current.IsPointerOverGameObject() is false)
                    return true;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
}
