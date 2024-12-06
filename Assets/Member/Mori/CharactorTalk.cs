using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

//Mori Script

public class CharactorTalk : MonoBehaviour
{
    [SerializeField]
    private PlaySceneDatas playSceneDatas;
    [Header("会話文")]
    [SerializeField, Label("会話前テキスト")]
    public string _helloTxt;
    [SerializeField, Label("受注テキスト"),TextArea]
    public List<string> _orderTxt;
    [SerializeField, Label("達成テキスト"),TextArea]
    public List<string> _clearTxt;

    [SerializeField,Label("テキスト")] 
    private TMP_Text tmpText;
    [SerializeField,Label("文字送り速度")] 
    private float _txtSpeed;
    [SerializeField, Label("テキストボックス元サイズ")]
    private Vector2 _beforeSize = new Vector2(17, 5);
    [SerializeField, Label("テキストボックスサイズ")]
    private Vector2 _afterSize;
    [SerializeField]
    private SpriteRenderer _boxSR;
    [SerializeField]
    private Button _button;

    public bool _isClear = false;

    private int _orderIndex = 0;
    private int _clearIndex = 0;

    private Canvas _canvas;


    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _orderIndex = 0;
        _clearIndex = 0;
        _button.gameObject.SetActive(false);

        //debug
        //条件はクエストの受注状況参照
        if (_isClear)
        {
            _button.onClick.AddListener(ClearText);
        }
        else
        {
            _button.onClick.AddListener(OrderText);
        }
    }

    void Update()
    {
    }

    [SerializeField,Button]
    public async void HelloText()
    {
        SetMaxBoxSize();
        await Task.Delay((int)1f);
        tmpText.text = _helloTxt;
        TxtAnim();
    }

    [SerializeField, Button]
    public void ByeText()
    {
        tmpText.text = "?";
        SetMinBoxSize();
    }

    [SerializeField, Button]
    public async void OrderText()
    {
        if (_orderIndex >= _orderTxt.Count)
        {
            //会話終わり処理
            _orderIndex = 0;
        }
        SetMaxBoxSize();
        await Task.Delay((int)1f);
        tmpText.text = _orderTxt[_orderIndex];
        TxtAnim();
        _orderIndex++;
    }

    [SerializeField, Button]
    public async void ClearText()
    {
        if (_clearIndex >= _clearTxt.Count)
        {
            //会話終わり処理
            _clearIndex = 0;
            //以降会話不可
            //完了かっといん
        }
        SetMaxBoxSize();
        await Task.Delay((int)1f);
        tmpText.text = _clearTxt[_clearIndex];
        TxtAnim();
        _clearIndex++;
    }


    [SerializeField, Button]
    public async void SetMaxBoxSize()
    {
        if (_boxSR.size == _afterSize) return;
        if (_beforeSize.y!= _afterSize.y)
        {
            await DOVirtual.Float(
                from: _beforeSize.y, to: _afterSize.y, duration: 0.2f,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(_beforeSize.x, tweenValue); });
        }
        
        await DOVirtual.Float(
            from: _beforeSize.x, to: _afterSize.x, duration: 0.3f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(tweenValue, _afterSize.y); });
    }
    [SerializeField, Button]
    public async void SetMinBoxSize()
    {
        if (_boxSR.size == _beforeSize) return;
        if (_beforeSize.y != _afterSize.y)
        {
            await DOVirtual.Float(
                from: _afterSize.y, to: _beforeSize.y, duration: 0.1f,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(_afterSize.x, tweenValue); });
        }

        await DOVirtual.Float(
            from: _afterSize.x, to: _beforeSize.x, duration: 0.1f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(tweenValue, _beforeSize.y); });
    }


    [SerializeField, Button]
    public void TxtAnim()
    {
        StartCoroutine(Simple());
    }

    private IEnumerator Simple()
    {
        // 文字の表示数を0に(テキストが表示されなくなる)
        tmpText.maxVisibleCharacters = 0;

        // テキストの文字数分ループ
        for (var i = 0; i < tmpText.text.Length; i++)
        {
            // 一文字ごとに0.2秒待機
            yield return new WaitForSeconds(_txtSpeed);

            // 文字の表示数を増やしていく
            tmpText.maxVisibleCharacters = i + 1;
        }
    }
}
