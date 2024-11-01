using NaughtyAttributes;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class AppleController : MonoBehaviour
{
    // 消えたAppleの数を数える
    // 取ったAppleの数が15個だったら達成それ以外の数字なら失敗をgameManagerに伝える

    public static AppleController instance;

    public GameObject canvas;

    public TextMeshProUGUI textBasketNum;

    public static int Count = 4;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(canvas);
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

}