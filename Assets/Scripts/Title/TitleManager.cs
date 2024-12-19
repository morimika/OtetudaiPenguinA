using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button        _menuButton;
    [SerializeField] private Button        _nextSceneButton;
    [SerializeField] private LoadNextScene _loadNextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        //  メニューボタン押しの処理を関連付ける
        _menuButton ? .onClick.AddListener(OnMenuButtonClicked);
        //_nextSceneButton ? .onClick.AddListener(OnNextSceneButtonClicked);
    }

    /// <summary>
    /// メニューボタン押し
    /// </summary>
    private void OnMenuButtonClicked()
    {
        Debug.Log("Menu button clicked");
    }

    [SerializeField] private AnimTestScripts _animTestScripts;
    private void OnNextSceneButtonClicked()
    {
        _loadNextScene.LoadScene();
    }
}
