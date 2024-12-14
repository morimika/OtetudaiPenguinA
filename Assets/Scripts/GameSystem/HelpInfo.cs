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

    private bool _onMouse = false;
    private bool _isIn = false;

    private CharactorTalk charactorTalk;

    [SerializeField]
    private float _distance = 4;
    [SerializeField]
    private PlaySceneDatas _playSceneDatas;

    public bool isEnd = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        charactorTalk=GetComponent<CharactorTalk>();
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
            if (!_isIn && !isEnd)
            {
                charactorTalk.HelloText();
                _isIn = true;
            }
            else if(!_isIn && isEnd)
            {
                charactorTalk.EndText();
                _isIn = true;
            }
            //対象をクリックしたとき
            if (Input.GetMouseButtonDown(0) && _onMouse && _playSceneDatas.TapType==PlaySceneTapType.Play && !isEnd)    
            {
                //既に成功している場合
                if (IsClear==true && HavingHelpTask == kind.ToString())
                {
                    charactorTalk.ClearText();
                }
                //まだ成功していない
                //受注テキスト処理
                else if(IsClear==false)
                {
                    charactorTalk.OrderText();
                    //タスク受注
                    HavingHelpTask = kind.ToString();
                }
            }
        }
        //出るとき1フレームだけ呼び
        else if (Vector3.Distance(this.transform.position, _playerPos) > _distance && _isIn && !isEnd)
        {
            charactorTalk.ByeText();
            _isIn = false;
        }
        else if(Vector3.Distance(this.transform.position, _playerPos) > _distance && _isIn && isEnd)
        {
            charactorTalk.Stay();
            _isIn = false;
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
