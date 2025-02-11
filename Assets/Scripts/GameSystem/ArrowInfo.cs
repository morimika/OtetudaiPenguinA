using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;

//Mori Scpipt

public class ArrowInfo : MonoBehaviour
{
    // 動かすオブジェクトのトランスフォーム
    [SerializeField]
    private Transform _arrowTra;
    // ターゲットのオブジェクトのトランスフォーム
    private Transform _targetTra;

    private GameObject _player;

    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private List<OtetudaiFade> _startObjScr;
    [SerializeField]
    private PlaySceneDatas _playSceneDatas;

    private string haveTask=" ";

    private string tar;

    // Start is called before the first frame update
    void Start()
    {
        _arrowTra = transform.GetChild(0).gameObject.transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] startObj = GameObject.FindGameObjectsWithTag("StartObj");
        for (int i = 0; i < startObj.Length; i++)
        {
            _startObjScr.Add(startObj[i].GetComponent<OtetudaiFade>());
            Debug.Log(startObj[i].name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの位置に合わせる
        _arrowTra.position = _player.transform.position;
        //Play中以外は表示しない
        if (_playSceneDatas.TapType != PlaySceneTapType.Play)
        {
            _arrowTra.gameObject.SetActive(false);
        }
        //クリアしているとき、または、お手伝いが無いときは何もしない
        else if (HelpManager.IsClear == true || HelpManager.HavingHelpTask == null)
        {
            _arrowTra.gameObject.SetActive(false);
            _targetTra = null;
            return;
        }
        //目標地点が無いとき、または、お手伝いが更新されたとき、目標地点設定
        else if(_targetTra==null || haveTask!=HelpManager.HavingHelpTask)
        {
            for(int i=0;i< _startObjScr.Count; i++)
            {
                tar =_startObjScr[i]._helpInfo.kind.ToString();
                if (tar == HelpManager.HavingHelpTask.ToString())
                {
                    _targetTra = _startObjScr[i].gameObject.transform;
                    //break;
                }
            }
        }
        haveTask = HelpManager.HavingHelpTask;
        if (_targetTra == null) return;
        //目標地点までの距離が_distance以上の場合
        if (Vector3.Distance(_arrowTra.position, _targetTra.position) >= _distance)
        {
            _arrowTra.gameObject.SetActive(true);
            // ターゲットまでのベクトルを求める
            Vector3 direction = _targetTra.position - _arrowTra.transform.position;
            // 角度を求める。
            float angle = Mathf.Atan2(direction.x, direction.y);
            //オブジェクトをQuaternion.AngleAxisを使って回転させる。
            _arrowTra.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.back);
        }
        else
        {
             _arrowTra.gameObject.SetActive(false);
        }
    }
}
