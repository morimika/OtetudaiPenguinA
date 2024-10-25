using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PocketButton : MonoBehaviour
{
    [SerializeField, Label("ポケットタブオブジェクト")]
    private RectTransform _pocketUI;
    [SerializeField, Label("ポケット閉じ不可視ボタン")]
    private GameObject _closeButton;

    [ReadOnly]
    private bool _isPocketOpen = false;

    private enum Scenes
    { 
        Yuria_TitleScene,
        Yuria_PlayScene,
        Mori_MainGameScene,
        Mori_FreeBook
    }


    // Start is called before the first frame update
    void Start()
    {
        _isPocketOpen = false;
        _closeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ChangeFreeBook()
    {
        SceneManager.LoadScene(nameof(Scenes.Mori_FreeBook));
    }
    public void ChangeTwo()
    {

    }
}
