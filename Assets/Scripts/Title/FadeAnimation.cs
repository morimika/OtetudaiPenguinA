using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField,Header("シーン遷移スクリプト")] private ChangeScene _changeScene;
    [SerializeField,Header("フェードインアニメーションスクリプト")] private Canvas _changeAnimation;
    private Scene _nextScene;
    public bool _fadeInEnd;

    
    // Update is called once per frame
    void Update()
    {
        OnAnimActive();
        
    }

    private void OnAnimActive()
    {
        if (ChangeScene._isTap)
        {
            if (PanelManager._isPaused)
            {
                //return;
            }
            else
            {
                //アニメーション終了判定
                _fadeInEnd = true;
                _changeScene.OnStartTap();
                Debug.Log(_fadeInEnd);
                //キャンバスをアクティブにする
                _changeAnimation.gameObject.SetActive(true);
                
            }
        }
        
        
        
    }
}
