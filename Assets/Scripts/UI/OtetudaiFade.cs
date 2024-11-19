using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using NaughtyAttributes;
using System;

//Mori Script

public class OtetudaiFade : MonoBehaviour
{
    private Vector3 _playerPos;
    private GameObject _player;

    private bool _onMouse = false;

    [SerializeField]
    private GameObject _action;

    [SerializeField, Scene]
    private int _sceneIndex;

    [SerializeField]
    private FadeView _fadeView;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        //_fadeView= GameObject.Find("FadeView").GetComponent<FadeView>();
    }

    void Update()
    {
        //プレイヤーのポジションを更新
        _playerPos = _player.transform.position;
        //距離を測って物と近いか確認
        if (Vector3.Distance(this.transform.position,_playerPos)<=3)
        {
            //押せるUI出現
            _action.SetActive(true);
            //物をクリックしたとき
            if (Input.GetMouseButtonDown(0) && _onMouse)
            {
                //フェードを待ち、遷移する
                PlayerSetPos.PlayerPos = _playerPos;
                ChangeScene.Instance.LoadNextScene(_sceneIndex);
            }
        }
        else
        {
            _action.SetActive(false);
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
