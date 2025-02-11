using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public static bool IsHiddenButton = false;
    [SerializeField, Label("ポケット表示ボタン")]
    private GameObject _pocketButton;
    private Button button;
    [SerializeField]
    private Sprite _openPocSp;
    [SerializeField]
    private Sprite _closePocSp;
    private Image _pocImage;

    [SerializeField, Label("絵本ウィンドウ")]
    private GameObject _picturebook;

    [SerializeField, Label("自由帳ウィンドウ")]
    private GameObject _freebook;

    [SerializeField, Label("設定画面アイコン")]
    private GameObject _optionCanvas;

    [SerializeField,Label("プレイヤーデータアセット")]
    private PlaySceneDatas _playSceneDatas;

    [SerializeField,ReadOnly]
    private bool _isPocketOpen = false;

    public static bool IsTatchAbleButton = true;

    // Start is called before the first frame update
    void Start()
    {
        //初期設定
        _isPocketOpen = false;
        _closeButton.SetActive(false);
        IsHiddenButton = false;
        _pocImage=_pocketButton.GetComponent<Image>();
        _optionCanvas.SetActive(false);
        button = _pocketButton.GetComponent<Button>();
    }
 
    private void Update()
    {
        Debug.Log(_playSceneDatas.TapType.ToString());
        if (IsHiddenButton)
        {
            _pocketButton.SetActive(false);
        }
        else
        {
            _pocketButton.SetActive(true);
        }

        if (IsTatchAbleButton)
        {
            button.enabled = true;
        }
        else
        {
            button.enabled = false;
        }
    }
 
    /// <summary>
    /// ポケットを開く
    /// </summary>
    public void OpenPocket()
    {
        _playSceneDatas.TapType = PlaySceneTapType.Pose;
        StartCoroutine(OpenPoketSp());
    }

    public IEnumerator OpenPoketSp()
    {
        _pocImage.sprite = _openPocSp;
        yield return new WaitForSeconds(0.1f);
        _pocketUI.DOAnchorPos(new Vector2(0, 0), 0.5f);
        _isPocketOpen = true;
        _closeButton.SetActive(true);
        _optionCanvas.SetActive(true);
    }
 
    /// <summary>
    /// ポケットを閉じる
    /// </summary>
    public void ClosePocket()
    {
        _pocImage.sprite = _closePocSp;
        _pocketUI.DOAnchorPos(new Vector2(600, 0), 0.5f);
        _isPocketOpen = false;
        _closeButton.SetActive(false);
        _optionCanvas.SetActive(false);
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
        _pocImage.sprite = _closePocSp;
        _freebook.SetActive(false);
        _picturebook.SetActive(false);
        _optionCanvas.SetActive(false);
        _playSceneDatas.TapType = PlaySceneTapType.Play;
        PanelManager._isPaused = false;
    }

}

