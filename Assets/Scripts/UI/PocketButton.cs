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
 
    [SerializeField,ReadOnly]
    private bool _isPocketOpen = false;
 
    // Start is called before the first frame update
    void Start()
    {
        _isPocketOpen = false;
        _closeButton.SetActive(false);
        IsHiddenButton = false;
    }
 
    private void Update()
    {
        if (IsHiddenButton)
        {
            _pocketButton.SetActive(false);
        }
        else
        {
            _pocketButton.SetActive(true);
        }
    }
 
    public void OpenPocket()
    {
        _pocketUI.DOAnchorPos(new Vector2(0,0),0.5f);
        _isPocketOpen=true;
        _closeButton.SetActive(true);
    }
 
    public void ClosePocket()
    {
        _pocketUI.DOAnchorPos(new Vector2(600, 0), 0.5f);
        _isPocketOpen = false;
        _closeButton.SetActive(false);
    }
}

