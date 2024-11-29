using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HelpStart : MonoBehaviour
{
    private Vector3 _playerPos;
    private GameObject _player;

    [SerializeField]
    private GameObject _action;

    private bool _onMouse = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _playerPos = _player.transform.position;
        if (Vector3.Distance(this.transform.position, _playerPos) <= 3)
        {
            if (HelpManager.helpNameEnum == HelpManager.HelpNameEnum.None)
            {
                _action.SetActive(true);
                if (Input.GetMouseButtonDown(0) && _onMouse)
                {
                    HelpManager.helpNameEnum = HelpManager.HelpNameEnum.AppleGet;
                }
            }
            else if(HelpManager.helpNameEnum == HelpManager.HelpNameEnum.AppleGet)
            {
                if (HelpManager.isClear == true)
                {
                    _action.SetActive(false);
                }
            }
        }
        else
        {
            _action.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        _onMouse = true;
    }
    private void OnMouseUp()
    {
        _onMouse = false;
    }
}
