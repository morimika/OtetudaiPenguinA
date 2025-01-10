using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

public class PointerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointIcon;

    [SerializeField]
    private PlaySceneDatas _playSceneDatas;

    private GameObject obj;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_playSceneDatas.TapType == PlaySceneTapType.Play)
            {
                if (obj != null)
                {
                    Destroy(obj.gameObject);
                }
                // スクリーン座標をマウスから取得（Unityエディタ用）  
                var mousePos = Input.mousePosition;
                // ワールド座標に変換  
                var worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
                obj = Instantiate(_pointIcon, new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
            }

        }

        if(obj!=null)
        {
            var ppos = _player.transform.position;
            if (Vector2.Distance(obj.transform.position, ppos) < 1)
            {
                Destroy(obj.gameObject);
            }
        }
        if (_playSceneDatas.TapType != PlaySceneTapType.Play)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
            return;
        }
    }
}
