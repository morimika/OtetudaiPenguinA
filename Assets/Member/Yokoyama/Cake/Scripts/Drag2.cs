using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag2 : MonoBehaviour
{
    //ドラック開始フラグ
    public bool DragOn;

    private bool start = false;

    private Food food;
    private GameObject Manager;
    private CakeManager cakeMa;

    private void Start()
    {
        food = GetComponent<Food>();
        Manager = GameObject.FindGameObjectWithTag("CakeMa");
        cakeMa = Manager.GetComponent<CakeManager>();
    }

    private void Update()
    {
        if(cakeMa.mode == CakeManager.Mode.Game)
        {
            start = true;
        }
    }

    //オブジェクトをクリックしてドラッグ状態にある間呼び出される関数（Unityのマウスイベント）
    void OnMouseDrag()
    {
        if(start)
        {
            DragOn = true;
            food.stop = true;

            MoucePos();
        }
    }

    //マウスを離した時
    void OnMouseUp()
    {
        if(start)
        {
            DragOn = false;
        }
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
