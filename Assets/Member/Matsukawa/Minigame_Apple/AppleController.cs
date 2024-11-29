using NaughtyAttributes;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

// Matsukawa

public class AppleController : MonoBehaviour
{
    // 消えたAppleの数を数える
    // 取ったAppleの数が15個だったら達成それ以外の数字なら失敗をgameManagerに伝える

    #region インスペクター上
    public static AppleController instance;
    public TextMeshProUGUI textBasketNum;
    public static int Count = 7;

    [Foldout("りんごのrb")] public GameObject apple1;
    [Foldout("りんごのrb")] public GameObject apple2;
    [Foldout("りんごのrb")] public GameObject apple3;
    [Foldout("りんごのrb")] public GameObject apple4;
    [Foldout("りんごのrb")] public GameObject apple5;
    [Foldout("りんごのrb")] public GameObject apple6;
    [Foldout("りんごのrb")] public GameObject apple7;
    [Foldout("りんごのrb")] public GameObject apple8;
    [Foldout("りんごのrb")] public GameObject apple9;
    [Foldout("りんごのrb")] public GameObject apple10;
    [Foldout("りんごのrb")] public GameObject apple11;
    [Foldout("りんごのrb")] public GameObject apple12;

    private int tree1 = 4;
    private int tree2 = 3;
    private int tree3 = 5;

    public List<Rigidbody> applelist = new List<Rigidbody>();


    #endregion

    public void Start()
    {
        CountBasketAppleNum();
    }

    // Update is called once per frame
    void Update()
    {
        // 現在合計いくつのappleを取得しているか数える
        CountBasketAppleNum();
    }

    // はさみが木に触れたときりんごがおちる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region appleのrigidbody　GetComponent
        Rigidbody apple1rb = apple1.GetComponent<Rigidbody>();
        Rigidbody apple2rb = apple2.GetComponent<Rigidbody>();
        Rigidbody apple3rb = apple3.GetComponent<Rigidbody>();
        Rigidbody apple4rb = apple4.GetComponent<Rigidbody>();
        Rigidbody apple5rb = apple5.GetComponent<Rigidbody>();
        Rigidbody apple6rb = apple6.GetComponent<Rigidbody>();
        Rigidbody apple7rb = apple7.GetComponent<Rigidbody>();
        Rigidbody apple8rb = apple8.GetComponent<Rigidbody>();
        Rigidbody apple9rb = apple9.GetComponent<Rigidbody>();
        Rigidbody apple10rb = apple10.GetComponent<Rigidbody>();
        Rigidbody apple11rb = apple11.GetComponent<Rigidbody>();
        Rigidbody apple12rb = apple12.GetComponent<Rigidbody>();
        #endregion
        if (collision.gameObject.tag == "Tree1")
        {
            Count = Count + tree1;
            applelist[0].isKinematic = false;
            apple1rb.isKinematic = false;
            apple2rb.isKinematic = false;
            apple3rb.isKinematic = false;
            apple4rb.isKinematic = false;

        }
        else if (collision.gameObject.tag == "Tree2")
        {
            Count = Count + tree2;
            apple5rb.isKinematic = false;
            apple6rb.isKinematic = false;
            apple7rb.isKinematic = false;

        }
        else if (collision.gameObject.tag == "Tree3")
        {
            /*
            Count = Count + tree3;
            for
            apple8rb.isKinematic = false;
            apple9rb.isKinematic = false;
            apple10rb.isKinematic = false;
            apple11rb.isKinematic = false;
            apple12rb.isKinematic = false;
            */
        }
    }

    private void Reset()
    {
        // リンゴの数をリセット
        Count = 7;
        // リンゴの表示をリセット
        //apple1.SetActive(true);
        //apple2.SetActive(true);
        //apple3.SetActive(true);
    }

    void kanss(Rigidbody rb, GameObject a)
    {
        rb = a.GetComponent<Rigidbody>();
        rb.isKinematic = false;

    }

    public void CountBasketAppleNum()
    {
        // appleの総数表示
        textBasketNum.text = Count.ToString("0");
        Debug.Log(Count);
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