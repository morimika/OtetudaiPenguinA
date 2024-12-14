using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
 
//Mori Script
 
/// <summary>
/// ポケットを開閉するスクリプト
/// 要変更
/// </summary>
public class PocketButton : MonoBehaviour
{
    [SerializeField, Label("ポケットタブオブジェクト")]
    private RectTransform _pocketUI;
    [SerializeField, Label("ポケット閉じ不可視ボタン")]
    private GameObject _closeButton;
 
    [SerializeField, Label("ポケットボタン表示bool")]
    public bool IsHiddenButton = false;
    [SerializeField, Label("ポケット表示ボタン")]
    private GameObject _pocketButton;

    [SerializeField, Label("絵本ウィンドウ")]
    private GameObject _picturebook;

    [SerializeField, Label("自由帳ウィンドウ")]
    private GameObject _freebook;

    [SerializeField,Label("プレイヤーデータアセット")]
    private PlaySceneDatas _playSceneDatas;

    [SerializeField,ReadOnly]
    private bool _isPocketOpen = false;
 
    // Start is called before the first frame update
    void Start()
    {
        //初期設定
        _isPocketOpen = false;
        _closeButton.SetActive(false);
        IsHiddenButton = false;
    }
 
    private void Update()
    {
        if (IsHiddenButton || _playSceneDatas.TapType!=PlaySceneTapType.Play)
        {
            _pocketButton.SetActive(false);
        }
        else
        {
            _pocketButton.SetActive(true);
        }
    }
 
    /// <summary>
    /// ポケットを開く
    /// </summary>
    public void OpenPocket()
    {
        _pocketUI.DOAnchorPos(new Vector2(0,0),0.5f);
        _isPocketOpen=true;
        _closeButton.SetActive(true);
        _playSceneDatas.TapType = PlaySceneTapType.Pose;
    }
 
    /// <summary>
    /// ポケットを閉じる
    /// </summary>
    public void ClosePocket()
    {
        _pocketUI.DOAnchorPos(new Vector2(600, 0), 0.5f);
        _isPocketOpen = false;
        _closeButton.SetActive(false);
        _playSceneDatas.TapType = PlaySceneTapType.Play;
    }

    /// <summary>
    /// 絵本を開く
    /// </summary>
    public void ShowPictureBook()
    {
        _picturebook.SetActive(true);
        PanelManager._isPaused = true;
        ClosePocket();
        _playSceneDatas.TapType = PlaySceneTapType.Pose;
    }

    /// <summary>
    /// 自由帳を開く
    /// </summary>
    public void ShowFreeBook()
    {
        _freebook.SetActive(true);
        PanelManager._isPaused = true;
        ClosePocket();
        _playSceneDatas.TapType = PlaySceneTapType.Pose;
    }

    /// <summary>
    /// ウィンドウを閉じる
    /// </summary>
    public void CloseWindow()
    {
        _freebook.SetActive(false);
        _picturebook.SetActive(false);
        _playSceneDatas.TapType = PlaySceneTapType.Play;
        PanelManager._isPaused = false;
    }

}

