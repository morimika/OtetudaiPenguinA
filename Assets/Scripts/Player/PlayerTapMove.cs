using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTapMove : MonoBehaviour
{
    [SerializeField,Header("移動速度")] private float _moveSpeed = 5f;
    [SerializeField,Header("ダッシュ速度")] private float _dashSpeed = 10f;
    //ダブルタップした間隔時間
    [SerializeField,Header("ダブルタップの間隔")] private float _doubTapTime = 0.2f;

    [SerializeField, Header(("現在がなんのタイプなのか"))]
    private PlaySceneDatas _playSceneDatas;

    //タップした場所
    private Vector3 _tapPos;
    //移動中判定
    private  bool _isMoving;
    //ダブルタップしているか判定
    private bool _isDoubleTapStart;
    private int _isTapMode = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //初期はPlayerに設定しておく
        _playSceneDatas.TapType = PlaySceneTapType.Player;
    }
    

    void Update()
    {
       MovePlayer();
       
    }

    /// <summary>
    /// タップ移動
    /// </summary>
    private void MovePlayer()
    {
        if (!PanelManager._isPaused)
        {
            if (_isDoubleTapStart)
            {
                _doubTapTime += Time.deltaTime;
                if (_doubTapTime <= 0.2f)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //  ダブルタップした時の処理を書く
                        Move();
                        _isTapMode = 2;
                        _isDoubleTapStart = false;
                        _doubTapTime = 0;
                        
                    }
                }
                else
                {
                    //  シングルタップした時の処理を書く
                    Move();
                    _isTapMode = 1;
                    _isDoubleTapStart = false;
                    _doubTapTime = 0;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isDoubleTapStart = true;
                }
            }
            if (_isMoving)
            {
                if (_isTapMode == 2)
                    DashMovePos();
                else
                    MovePos();
            }
        }
    }
    
/// <summary>
/// マウスのポジションの初期設定
/// </summary>
    private void Move()
    {
        if (!PanelManager._isPaused)
        {
            Debug.Log("動いている");
             _tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _tapPos.z = 0;
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
       
    }

/// <summary>
/// プレイヤーがどっち向いているかどうか
/// </summary>
    private void SetPlayerDirection()
    {
        if (Mathf.Abs(_tapPos.x) > Mathf.Abs(_tapPos.y))
        {
            if(_tapPos.x > 0) Debug.Log("Right");
            else Debug.Log("Left");
        }
        else
        {
            if(_tapPos.y > 0) Debug.Log("Up");
            else Debug.Log("Down");
        }
    }
    
    /// <summary>
    /// 移動処理
    /// </summary>
    private void MovePos()
    {
        SetPlayerDirection();
        transform.position = Vector2.MoveTowards(transform.position, _tapPos, _moveSpeed * Time.deltaTime);
        if (transform.position == _tapPos)
        {
            _isMoving = false;
        }
    }

    /// <summary>
    /// ダッシュ移動処理
    /// </summary>
    private void DashMovePos()
    {
        SetPlayerDirection();
        transform.position = Vector2.MoveTowards(transform.position, _tapPos, _dashSpeed * Time.deltaTime);
        if (transform.position == _tapPos)
        {
            _isMoving = false;
        }
    }

    /// <summary>
    /// プレイヤーが移動していいかどうかの判定
    /// </summary>
    /// <returns>モードがプレイヤーなら</returns>
    private bool IsTapEnable()
    {
        return _playSceneDatas.TapType.HasFlag(PlaySceneTapType.Player);
    }
}
