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

//Mori Script

public class CharactorTalk : MonoBehaviour
{
    #region 変数作成
    [SerializeField]
    private PlaySceneDatas playSceneDatas;

    [Header("会話文------------------------")]
    [SerializeField, Label("会話前テキスト")]
    public string _helloTxt;
    [SerializeField, Label("受注テキスト"),TextArea]
    public List<string> _orderTxt;
    [SerializeField, Label("達成テキスト"),TextArea]
    public List<string> _clearTxt;
    [SerializeField, Label("達成後テキスト")]
    public string _endTxt;

    [Header("会話文------------------------")]

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
    private Sprite _txtboxSp;
    [SerializeField]
    private Sprite _cloudSp;

    [SerializeField]
    private Button _clearButton;
    [SerializeField]
    private Button _orderButton;


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
    private HelpInfo helpInfo;
    #endregion

    void Start()
    {
        //初期取得
        _player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _virtualCamera 
            = GameObject.FindGameObjectWithTag("VirtualCamera")
            .GetComponent<CinemachineVirtualCamera>();
        cutInManager
            = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<CutInManager>();
        helpInfo = GetComponent<HelpInfo>();
        //会話初期化
        _orderIndex = 0;
        _clearIndex = 0;
        //会話ボタン無効化
        _clearButton.gameObject.SetActive(false);
        _orderButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if ((playSceneDatas.TapType != PlaySceneTapType.Talk
            && playSceneDatas.TapType != PlaySceneTapType.Pose)
            || HelpManager.IsEndBool[(int)helpInfo.kind] == false)
        {
            //NPCタッチ可
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(HelpManager.IsEndBool[(int)helpInfo.kind] == true)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public async void QuestText()
    {
        SetQuestEnableBox();
        await Task.Delay((int)0.5f);
        tmpText.text = "?";
    }

    [SerializeField,Button]
    public async void HelloText()
    {
        //_boxSR.sprite = _txtboxSp;
        SetMaxBoxSize();
        await Task.Delay((int)1f);
        tmpText.text = _helloTxt;
        TxtAnim();
    }

    [SerializeField, Button]
    public void ByeText()
    {
        //_boxSR.sprite = _cloudSp;
        tmpText.text = "?";
        SetMinBoxSize();
    }

    //debug
    [SerializeField, Button]
    public void EndText()
    {
        //_boxSR.sprite = _txtboxSp;
        tmpText.text = _endTxt;
        SetEnableBox();
    }

    //debug
    [SerializeField, Button]
    public void Stay()
    {
        tmpText.text = "";
        SetDisBox();
    }

    [SerializeField, Button]
    public async void OrderText()
    {
        //_boxSR.sprite = _txtboxSp;
        _orderButton.interactable = false;
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
            _orderButton.gameObject.SetActive(true);
            _orderButton.transform.position = gameObj.transform.position;
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
            _orderButton.gameObject.SetActive(false);

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

            //タスク受注
            HelpManager.HavingHelpTask = helpInfo.kind.ToString();
            HelpManager.IsClear = false;

            //会話進行度初期化
            _orderIndex = 0;

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
        _orderButton.interactable = true;
    }

    [SerializeField, Button]
    public async void ClearText()
    {
        //_boxSR.sprite = _txtboxSp;
        _clearButton.interactable = false;

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
            _clearButton.gameObject.SetActive(true);
            _clearButton.transform.position = gameObj.transform.position;
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
            if (_clearIndex > _clearTxt.Count)
            {
                //会話終わり処理
                //ボタンを非表示
                _clearButton.gameObject.SetActive(false);
                await Task.Delay(2000);
                //シール獲得カットイン
                cutInManager.SealCutIn();

                //自由帳処理(自動)
                //自由帳出現

                //指定の物を大きくフェードインして　指定のものとは？

                //縮小しながら位置へ

                //少し見せてから自由帳フェードアウト


                //終わったら
                //プレイヤーにフォーカスを戻す
                _virtualCamera.Follow = _player.transform;
                //カメラズームアウト
                await DOVirtual.Float(
                    from: 4, to: 5, duration: 1,
                    //値が変わった時の処理
                    onVirtualUpdate: (tweenValue)
                    => { _virtualCamera.m_Lens.OrthographicSize = tweenValue; });

                //お手伝い状況リセット
                HelpManager.HavingHelpTask = null;
                HelpManager.IsClear = false;
                //ポーチ出現
                PocketButton.IsHiddenButton = false;
                playSceneDatas.TapType = PlaySceneTapType.Play;

                //カメラ制御のゲームオブジェクトを削除
                Destroy(gameObj.gameObject);

                _clearIndex = 0;

                //以降会話不可
                HelpManager.IsEndBool[(int)helpInfo.kind] = true;
                EndText();
            }
            else if (_clearIndex == _clearTxt.Count)
            {
                //完了かっといん
                cutInManager.ClearCutIn();
                await Task.Delay(2000);
                _clearIndex++;
            }
        }
        else
        {
            tmpText.text = _clearTxt[_clearIndex];
            TxtAnim();
            await Task.Delay(tmpText.text.Length * 50);
            _clearIndex++;
        }
        _clearButton.interactable = true;
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
    public async void SetDisBox()
    {
        if (_boxSR.size == _beforeSize) return;
        tmpText.gameObject.SetActive(false);
        if (_beforeSize.x != _afterSize.x)
        {
            await DOVirtual.Float(
                from: _afterSize.x, to: 16.5f, duration: 0.1f,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(tweenValue, _afterSize.y); });
        }

        await DOVirtual.Float(
            from: _afterSize.y, to: 5, duration: 0.2f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(16.5f, tweenValue); });

        await DOVirtual.Float(
            from: 1, to: 0, duration: 0.1f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.color = new Color(255,255,255,tweenValue); });
    }

    [SerializeField, Button]
    public async void SetEnableBox()
    {
        if (_boxSR.size == _afterSize) return;
        tmpText.gameObject.SetActive(false);
        await DOVirtual.Float(
            from: 0, to: 1, duration: 0.2f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.color = new Color(255, 255, 255, tweenValue); });
        if (_beforeSize.y != _afterSize.y)
        {
            await DOVirtual.Float(
                from: 5, to: _afterSize.y, duration: 0.2f,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(16.5f, tweenValue); });
        }

        await DOVirtual.Float(
            from: 16.5f, to: _afterSize.x, duration: 0.3f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(tweenValue, _afterSize.y); });
        tmpText.gameObject.SetActive(true);
        TxtAnim();
    }

    [SerializeField, Button]
    public async void SetQuestEnableBox()
    {
        if (_boxSR.size == _afterSize) return;
        tmpText.gameObject.SetActive(false);
        await DOVirtual.Float(
            from: 0, to: 1, duration: 0.2f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.color = new Color(255, 255, 255, tweenValue); });
        if (_beforeSize.y != _afterSize.y)
        {
            await DOVirtual.Float(
                from: 5, to: _beforeSize.y, duration: 0.2f,
                //値が変わった時の処理
                onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(16.5f, tweenValue); });
        }

        await DOVirtual.Float(
            from: 16.5f, to: _beforeSize.x, duration: 0.3f,
            //値が変わった時の処理
            onVirtualUpdate: (tweenValue) => { _boxSR.size = new Vector2(tweenValue, _beforeSize.y); });
        tmpText.gameObject.SetActive(true);
        TxtAnim();
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
