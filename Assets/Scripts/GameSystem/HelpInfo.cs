using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

//Mori sprict

public class HelpInfo : HelpManager
{
    /// <summary>
    /// 各お手伝い内容
    /// </summary>
    [Header("お手伝い内容")]
    public HelpKind kind;
    private Vector3 _playerPos;
    private GameObject _player;
    private Player _playerScr;

    private bool _onMouse = false;
    private bool _isIn = false;

    private CharactorTalk charactorTalk;

    [SerializeField]
    private float _distance = 4;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private PlaySceneDatas _playSceneDatas;

    [SerializeField,Label("取得できるシールを設定")]
    private ItemData _itemData;
    [SerializeField]
    private ItemList _playerItem;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        charactorTalk=GetComponent<CharactorTalk>();
        _playerScr=_player.GetComponent<Player>();
        _boxCollider=GetComponent<BoxCollider2D>();
        _boxCollider.enabled = false;
        //お手伝いが終わっていなければ
        if (!IsEndBool[(int)kind])
        {
            charactorTalk.QuestText();
        }
    }

    void Update()
    {
        //プレイヤーのポジションを更新
        _playerPos = _player.transform.position;
        //距離を測って物と近いか確認
        //範囲内に入ったとき
        if (Vector3.Distance(this.transform.position, _playerPos) <= _distance)
        {
            //入った1フレームだけテキスト処理呼び
            if (!_isIn && !IsEndBool[(int)kind])
            {
                _boxCollider.enabled = true;
                charactorTalk.HelloText();
                _isIn = true;
            }
            else if(!_isIn && IsEndBool[(int)kind])
            {
                charactorTalk.EndText();
                _isIn = true;
            }
            //対象をクリックしたとき(話しかけたとき)
            if (Input.GetMouseButtonDown(0) && _onMouse && _playSceneDatas.TapType==PlaySceneTapType.Play && !IsEndBool[(int)kind])    
            {
                _playerScr.Moov_Finish();
                //既に成功している場合
                if (IsClear==true && HavingHelpTask == kind.ToString())
                {
                    charactorTalk.ClearText();
                    _playerItem.items.Add(_itemData);
                    HavingHelpTask = null;
                }
                //まだ成功していない
                //受注テキスト処理
                else if(IsClear==false)
                {
                    charactorTalk.OrderText();
                }
            }
        }
        //出るとき1フレームだけ呼び
        else if (Vector3.Distance(this.transform.position, _playerPos) > _distance && _isIn && !IsEndBool[(int)kind])
        {
            _boxCollider.enabled = false;
            charactorTalk.ByeText();
            _isIn = false;
        }
        else if(Vector3.Distance(this.transform.position, _playerPos) > _distance && _isIn && IsEndBool[(int)kind])
        {
            _boxCollider.enabled = false;
            charactorTalk.Stay();
            _isIn = false;
        }
        else
        {
            _boxCollider.enabled = false;
        }
    }

    /// <summary>
    /// 押しているか
    /// </summary>
    private void OnMouseDown()
    {
        _onMouse = true;
    }
    private void OnMouseUp()
    {
        _onMouse = false;
    }
}
