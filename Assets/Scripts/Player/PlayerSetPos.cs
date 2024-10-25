using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

/// <summary>
/// プレイヤーの位置を保存するスクリプト
/// プレイヤーにアタッチする
/// </summary>
public class PlayerSetPos : MonoBehaviour
{
    //初期位置は0,0
    public static Vector2 PlayerPos = new Vector2(0, 0);

    void Start()
    {
        //保存した座標でスポーン
        this.transform.position = PlayerPos;
    }
}
