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
            if (!_isIn)
            {
                charactorTalk.HelloText();
                _isIn = true;
            }
            //対象をクリックしたとき
            if (Input.GetMouseButtonDown(0) && _onMouse)
            {
                //既に成功している場合
                if (IsClear && HavingHelpTask == kind.ToString())
                {
                    Debug.Log("HI");
                    charactorTalk.ClearText();
                }
                //まだ成功していないしていない
                //受注テキスト処理
                else if(!IsClear)
                {
                    Debug.LogError("HI");
                    charactorTalk.OrderText();
                    //タスク受注
                    HavingHelpTask = kind.ToString();
                }
            }
        }
        //出るとき1フレームだけ呼び
        else if (Vector3.Distance(this.transform.position, _playerPos) > _distance && _isIn)
        {
            charactorTalk.ByeText();
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
