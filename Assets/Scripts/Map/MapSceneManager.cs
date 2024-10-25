using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    //現在のタイプを取得
    [SerializeField] private PlaySceneDatas _playSceneDatas;
    // Start is called before the first frame update
    void Start()
    {
        //最初にプレイヤーに戻す
        _playSceneDatas.TapType = PlaySceneTapType.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
