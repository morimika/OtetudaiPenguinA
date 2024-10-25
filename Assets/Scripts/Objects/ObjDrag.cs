using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

/// <summary>
/// オブジェクトをドラッグでつかめるスクリプト
/// </summary>
public class ObjDrag : MonoBehaviour
{
    //座標用の変数
    private Vector3 mousePos, worldPos;

    //ドラッグされている間、ものをつかめる
    private void OnMouseDrag()
    {
        //マウス座標の取得
        mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //ワールド座標を自身の座標に設定
        transform.position = worldPos;
    }
    private void OnMouseUp()
    {

    }
}
