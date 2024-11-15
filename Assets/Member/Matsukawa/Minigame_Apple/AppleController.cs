using NaughtyAttributes;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Matsukawa

public class AppleController : MonoBehaviour
{
    // 消えたAppleの数を数える
    // 取ったAppleの数が15個だったら達成それ以外の数字なら失敗をgameManagerに伝える

    public static AppleController instance;
    public TextMeshProUGUI textBasketNum;
    public static int Count = 7;

    [SerializeField] int tree1 = 4;
    [SerializeField] int tree2 = 3;
    [SerializeField] int tree3 = 5;

    [SerializeField] GameObject apple1;
    [SerializeField] GameObject apple2;
    [SerializeField] GameObject apple3;

    // Start is called before the first frame update
    void Start()
    {
       // Instantiate(canvas);
        CountBasketAppleNum();
    }

    // Update is called once per frame
    void Update()
    {
        CountBasketAppleNum();
    }

    public void CountBasketAppleNum()
    {
        // 現在合計いくつのappleを取得しているか数える

        // appleの総数表示
        textBasketNum.text = Count.ToString("0");
        Debug.Log(Count);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Tree1")
        {
            Count = Count + tree1;
            Destroy(apple1);
        }
        else if (collision.gameObject.tag == "Tree2")
        {
            Count = Count + tree2;
            Destroy(apple2);
        }
        else if (collision.gameObject.tag == "Tree3")
        {
            Count = Count + tree3;
            Destroy(apple3);
        }
    }



    #region はさみをドラッグで動かす処理
    private Vector3 offset;

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    #endregion
}