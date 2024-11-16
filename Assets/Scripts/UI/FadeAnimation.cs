using System;
using System.Collections;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField,Header( "フェードインアニメーションスクリプト")] private Animator _changeAnimator;
    [SerializeField, Header("終了処理検出プログラム")]        private FadeView _fadeView;

    private bool _isFadeIn = false;
    
    private bool _isFadeEnd = false;
    public bool IsFadeEnd => _isFadeEnd;
    
    private void Start()
    {
        _fadeView.Setup(OnFadeEndComplete);
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeInASync()
    {
        _isFadeEnd = false;
        _isFadeIn = true;
        _changeAnimator.gameObject.SetActive(true);
        _changeAnimator.SetTrigger("FadeIn");
        Debug.Log("fade in");
        yield return null;
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOutASync()
    {
        _isFadeEnd = false;
        _isFadeIn = false;
        _changeAnimator.gameObject.SetActive(true);
        _changeAnimator.SetTrigger("FadeOut");
        yield return null;
    }

    /// <summary>
    /// フェードが終了したら呼び出される
    /// </summary>
    private void OnFadeEndComplete()
    {
        //  フェードインの場合あにめーしょんのパネルを非表示にする
        if(_isFadeIn)
            _changeAnimator.gameObject.SetActive(false);
        _isFadeEnd = true;
    }
}
