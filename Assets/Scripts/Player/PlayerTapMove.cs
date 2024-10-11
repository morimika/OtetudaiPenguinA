using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTapMove : MonoBehaviour
{
    [SerializeField,Header("移動速度")] private float _moveSpeed = 5f;
    [SerializeField,Header("ダッシュ速度")] private float _dashSpeed = 10f;
    //ダブルタップした間隔時間
    [SerializeField,Header("ダブルタップの間隔")] private float _doubTapTime = 0.2f;

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
        
    }
    

    void Update()
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
    

    private void Move()
    {
        _tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _tapPos.z = 0;
       
        _isMoving = true;
    }

    private void MovePos()
    {
        transform.position = Vector2.MoveTowards(transform.position, _tapPos, _moveSpeed * Time.deltaTime);
        if (transform.position == _tapPos)
        {
            _isMoving = false;
        }
    }

    private void DashMovePos()
    {
        transform.position = Vector2.MoveTowards(transform.position, _tapPos, _dashSpeed * Time.deltaTime);
        if (transform.position == _tapPos)
        {
            _isMoving = false;
        }
    }
}
