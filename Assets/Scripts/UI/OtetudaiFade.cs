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
    #region 変数作成
    private Vector3 _playerPos;
    private GameObject _player;

    private bool _onMouse = false;

    [SerializeField]
    private GameObject _action;

    [SerializeField, Scene]
    private int _sceneIndex;

    [SerializeField]
    private float _distance = 4;

    public HelpInfo _helpInfo;
    #endregion


    void Start()
    {
        //初期取得
        _player = GameObject.FindGameObjectWithTag("Player");
        _helpInfo= transform.parent.gameObject.transform.GetComponentInChildren<HelpInfo>();
    }

    void Update()
    {
        //プレイヤーのポジションを更新、取得
        _playerPos = _player.transform.position;
        //対応するお手伝いを受けていたら
        if(HelpManager.HavingHelpTask == _helpInfo.kind.ToString())
        {
            //距離を測ってプレイヤーと近いか確認
            if (Vector3.Distance(this.transform.position, _playerPos) <= _distance)
            {
                //押せるUI出現
                _action.SetActive(true);
                //物をクリックしたとき
                if (Input.GetMouseButtonDown(0) && _onMouse)
                {
                    //フェードを待ち、遷移する
                    PlayerSetPos.PlayerPos = _playerPos;
                    //debug
                    //シーン遷移、フェード処理による
                    ChangeScene.Instance.LoadNextScene(_sceneIndex);
                }
            }
            else
            {
                //debug
                _action.SetActive(false);
            }
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
