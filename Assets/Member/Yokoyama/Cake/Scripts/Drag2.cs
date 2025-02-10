using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag2 : MonoBehaviour
{
    //ドラック開始フラグ
    public bool DragOn;

    private Food food;

    private void Start()
    {
        food = GetComponent<Food>();
    }

    //オブジェクトをクリックしてドラッグ状態にある間呼び出される関数（Unityのマウスイベント）
    void OnMouseDrag()
    {
        DragOn = true;
        food.stop = true;

        MoucePos();
    }

    //マウスを離した時
    void OnMouseUp()
    {
        DragOn = false;
    }

    //マウスのポジション取得
    private void MoucePos()
    {
        //マウスカーソル及びオブジェクトのスクリーン座標を取得
        Vector3 objectScreenPoint =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        //スクリーン座標をワールド座標に変換
        Vector3 objectWorldPoint = Camera.main.ScreenToWorldPoint(objectScreenPoint);

        //オブジェクトの座標を変更する
        transform.position = objectWorldPoint;
    }
}
