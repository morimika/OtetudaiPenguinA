using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Cinemachine;
using UnityEditor.Tilemaps;

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

    /// <summary>
    /// x.player y.camera
    /// </summary>
    [SerializeField]
    private Vector2 _playerOffset;

    public int _orderIndex = 0;
    public int _clearIndex = 0;

    private Canvas _canvas;
    private CinemachineVirtualCamera _virtualCamera;
    private GameObject _player;
    private GameObject gameObj;

    private CutInManager cutInManager;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _virtualCamera 
            = GameObject.FindGameObjectWithTag("VirtualCamera")
            .GetComponent<CinemachineVirtualCamera>();
        _orderIndex = 0;
        _clearIndex = 0;
        _button.gameObject.SetActive(false);
        cutInManager 
            = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<CutInManager>();

        //debug
        //条件はクエストの受注状況参照
        if (HelpManager.IsClear)
        {
            _button.onClick.AddListener(ClearText);
        }
        else
        {
            _button.onClick.AddListener(OrderText);
        }
    }

    private void Update()
    {
        if (playSceneDatas.TapType != PlaySceneTapType.Talk
            && playSceneDatas.TapType != PlaySceneTapType.Pose)
        {
            //NPCタッチ可
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        Debug.Log(HelpManager.IsClear);
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

    public void ButtonEnable()
    {
        _button.gameObject.SetActive(true);
    }

    [SerializeField, Button]
    public async void OrderText()
    {
        _button.interactable = false;
        //最初に話しかけた
        if (_orderIndex == 0)
        {
            //プレイヤー操作不可
            playSceneDatas.TapType = PlaySceneTapType.Talk;
            //NPCタッチ不可
            this.GetComponent<BoxCollider2D>().enabled = false;
            //カメラ制御のオブジェクト生成
            if (gameObj == null)
            {
                gameObj = new GameObject("Follow Target");
            }
            //オブジェクトをNPCとプレイヤーの間に配置、カメラを合わせる
            gameObj.transform.position
                = new Vector2(this.transform.position.x + (_playerOffset.x / 2)
                            , this.transform.position.y + 1);
            _virtualCamera.Follow = gameObj.transform;
            //画面全体にボタンを出現させる
            _button.gameObject.SetActive(true);
            _button.transform.position = gameObj.transform.position;
            //プレイヤーの位置移動　終わるまで待つ
            await _player.transform.DOMove(new Vector2(transform.position.x+_playerOffset.x
                                    , transform.position.y + _playerOffset.y), 1);
            //少しズーム　終わるまで待つ
            await DOVirtual.Float(
                from: 5, to: 4, duration: 1,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) => { _virtualCamera.m_Lens.OrthographicSize = tweenValue; });

            //テキストボックスを大きくする
            SetMaxBoxSize();
        }

        //会話文をすべて出し終わったとき
        if (_orderIndex >= _orderTxt.Count)
        {
            //ボタンを非表示
            _button.gameObject.SetActive(false);
            //会話進行度初期化
            _orderIndex = 0;

            //カットイン
            cutInManager.StartCutIn();

            //プレイヤーにフォーカスを戻す
            _virtualCamera.Follow = _player.transform;

            //カメラのズームを戻す 戻るまで待つ
            await DOVirtual.Float(
                from: 4, to: 5, duration: 1,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue)
                => { _virtualCamera.m_Lens.OrthographicSize = tweenValue; });

            //カメラ制御のゲームオブジェクトを削除
            Destroy(gameObj.gameObject);
            //NPCタッチ可
            this.GetComponent<BoxCollider2D>().enabled = true;

        }
        //会話文がまだあるとき
        else
        {
            //テキストを更新
            tmpText.text = _orderTxt[_orderIndex];
            //テキストアニメーション
            TxtAnim();
            await Task.Delay(tmpText.text.Length * 50);
            //次のテキストへ
            _orderIndex++;
        }
        _button.interactable = true;
    }

    [SerializeField, Button]
    public async void ClearText()
    {
        _button.interactable = false;

        if (_clearIndex == 0)
        {
            //プレイヤー操作不可
            playSceneDatas.TapType = PlaySceneTapType.Talk;
            //NPCタッチ不可
            this.GetComponent<BoxCollider2D>().enabled = false;
            //カメラ制御のオブジェクト生成
            if (gameObj == null)
            {
                gameObj = new GameObject("Follow Target");
            }
            //オブジェクトをNPCとプレイヤーの間に配置、カメラを合わせる
            gameObj.transform.position
                = new Vector2(this.transform.position.x + (_playerOffset.x / 2)
                            , this.transform.position.y + 1);
            _virtualCamera.Follow = gameObj.transform;

            //画面全体にボタンを出現させる
            _button.gameObject.SetActive(true);
            _button.transform.position = gameObj.transform.position;
            //プレイヤーの位置移動　終わるまで待つ
            await _player.transform.DOMove(new Vector2(transform.position.x + _playerOffset.x
                                    , transform.position.y + _playerOffset.y), 1);

            //少しズーム
            await DOVirtual.Float(
                from: 5, to: 4, duration: 1,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue)
                => { _virtualCamera.m_Lens.OrthographicSize = tweenValue; });

            //テキストボックスを大きくする
            SetMaxBoxSize();
        }


        if (_clearIndex >= _clearTxt.Count)
        {
            //会話終わり処理
            //ボタンを非表示
            _button.gameObject.SetActive(false);
            _clearIndex = 0;

            //完了かっといん
            cutInManager.ClearCutIn();

            //プレイヤーにフォーカスを戻す
            _virtualCamera.Follow = _player.transform;

            await DOVirtual.Float(
                from: 4, to: 5, duration: 1,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) 
                => { _virtualCamera.m_Lens.OrthographicSize = tweenValue; });

            //カメラ制御のゲームオブジェクトを削除
            Destroy(gameObj.gameObject);

            //NPCタッチ可
            this.GetComponent<BoxCollider2D>().enabled = true;

            //お手伝い状況リセット
            HelpManager.HavingHelpTask = "";
            HelpManager.IsClear = false;

            //以降会話不可

        }
        else
        {
            tmpText.text = _clearTxt[_clearIndex];
            TxtAnim();
            await Task.Delay(tmpText.text.Length * 50);
            _clearIndex++;
        }
        _button.interactable = true;
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
