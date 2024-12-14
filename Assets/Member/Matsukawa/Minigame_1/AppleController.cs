using NaughtyAttributes;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

// Matsukawa

public class AppleController : MonoBehaviour
{
    // 消えたAppleの数を数える
    // 取ったAppleの数が15個だったら達成それ以外の数字なら失敗をgameManagerに伝える

    #region インスペクター上
    public static AppleController instance;
    public        TextMeshProUGUI textBasketNum;
    public static int             Count = 7;

    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple1;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple2;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple3;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple4;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple5;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple6;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple7;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple8;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple9;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple10;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple11;
    [Foldout("りんごのrb"), SerializeField] protected GameObject _apple12;

    // それぞれの木が持ってるりんごの数をあらかじめ設定
    private int _tree1 = 4;
    private int _tree2 = 3;
    private int _tree3 = 5;

    // かごの現在地を取得
    [SerializeField] private GameObject _basketPos;

    // かごの中にあるリンゴ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket1; // かごのリンゴ 10 以上で表示　リンゴは１つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket2; // かごのリンゴ 13 以上で表示　リンゴは２つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket3; // かごのリンゴ 15 以上で表示　リンゴは３つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket4; // かごのリンゴ 17 以上で表示　リンゴは６つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket5; // かごのリンゴ 17 以上で表示　リンゴは６つ

    #endregion

    // リンゴがおちた木をもう一度触れないようにする
    private bool _canTouchTree = true;
    private bool _canTouchTree2 = true;
    private bool _canTouchTree3 = true;

    // リンゴがかごに移動する速さ
    protected float _speed = 10.0f;

    // AppleGoInBaske()の関数を Groundスクリプト から呼ぶときに使う
    public static bool _appleGoInBasket  = true;
    public static bool _appleGoInBasket2 = false;
    public static bool _appleGoInBasket3 = false;

    // はさみのカットアニメーション
    private Animator _scissorCuttingAnimation;
    private float   _scissorAnimWait = 3.0f;

    public void Start()
    {
        CountBasketAppleNum();

        _scissorCuttingAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 現在合計いくつのappleを取得しているか
        CountBasketAppleNum();
        // 取得しているりんごの数によってかごの中にあるリンゴの画像が変わる
        ShowInBasketApples();
    }

    // はさみが木に触れたときりんごがおちる
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        #region appleのrigidbody　GetComponent
        Rigidbody apple1rb = _apple1.GetComponent<Rigidbody>();
        Rigidbody apple2rb = _apple2.GetComponent<Rigidbody>();
        Rigidbody apple3rb = _apple3.GetComponent<Rigidbody>();
        Rigidbody apple4rb = _apple4.GetComponent<Rigidbody>();
        Rigidbody apple5rb = _apple5.GetComponent<Rigidbody>();
        Rigidbody apple6rb = _apple6.GetComponent<Rigidbody>();
        Rigidbody apple7rb = _apple7.GetComponent<Rigidbody>();
        Rigidbody apple8rb = _apple8.GetComponent<Rigidbody>();
        Rigidbody apple9rb = _apple9.GetComponent<Rigidbody>();
        Rigidbody apple10rb = _apple10.GetComponent<Rigidbody>();
        Rigidbody apple11rb = _apple11.GetComponent<Rigidbody>();
        Rigidbody apple12rb = _apple12.GetComponent<Rigidbody>();
        #endregion

        if (collision.gameObject.tag == "Tree1" &&  _canTouchTree == true)
        {
            // = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_scissorAnimWait));

            // 木１に当たった時
            // 木１のリンゴの数とかごにある数字を足す
            Count = Count + _tree1;

            // りんごを落とす
            // applelist[0].isKinematic = false;
            apple1rb.isKinematic = false;
            apple2rb.isKinematic = false;
            apple3rb.isKinematic = false;
            apple4rb.isKinematic = false;

            // 一度リンゴが落ちた木はもう一度触れない
            _canTouchTree = false;
        }
        else if (collision.gameObject.tag == "Tree2" && _canTouchTree2)
        {
            Count = Count + _tree2;
            apple5rb.isKinematic = false;
            apple6rb.isKinematic = false;
            apple7rb.isKinematic = false;

            _canTouchTree2 = false;
        }
        else if (collision.gameObject.tag == "Tree3" && _canTouchTree3)
        {
            Count = Count + _tree3;
            
            apple8rb.isKinematic = false;
            apple9rb.isKinematic = false;
            apple10rb.isKinematic = false;
            apple11rb.isKinematic = false;
            apple12rb.isKinematic = false;

            _canTouchTree3 = false;
        }
    }

    /// <summary>
    /// 木から落ちたリンゴが
    /// かごに入るアニメーション
    /// </summary>
    public void ApplesGoInBascket(GameObject obj)
    {
        obj = this.gameObject;
        _speed = _speed * Time.deltaTime;

        //スタート位置、ターゲットの座標、速度
        obj.transform.position = Vector3.MoveTowards(
        obj.transform.position, _basketPos.transform.position, _speed);
    }

    //public void CallAGIB()
    //{
    //    #region appleの 現在地(Vector2)　取得
    //    Transform _apple1pos = _apple1.GetComponent<Transform>();
    //    Transform _apple2pos = _apple2.GetComponent<Transform>();
    //    Transform _apple3pos = _apple3.GetComponent<Transform>();
    //    Transform _apple4pos = _apple4.GetComponent<Transform>();
    //    Transform _apple5pos = _apple5.GetComponent<Transform>();
    //    Transform _apple6pos = _apple6.GetComponent<Transform>();
    //    Transform _apple7pos = _apple7.GetComponent<Transform>();
    //    Transform _apple8pos = _apple8.GetComponent<Transform>();
    //    Transform _apple9pos = _apple9.GetComponent<Transform>();
    //    Transform _apple10pos = _apple10.GetComponent<Transform>();
    //    Transform _apple11pos = _apple11.GetComponent<Transform>();
    //    Transform _apple12pos = _apple12.GetComponent<Transform>();
    //    #endregion

    //    if (_appleGoInBasket)
    //    {
    //        ApplesGoInBascket(_apple1);
    //        ApplesGoInBascket(_apple2);
    //        ApplesGoInBascket(_apple3);
    //        ApplesGoInBascket(_apple4);
    //    }
    //    if (_appleGoInBasket2)
    //    {
    //        ApplesGoInBascket(_apple5);
    //        ApplesGoInBascket(_apple6);
    //        ApplesGoInBascket(_apple7);
    //    }
    //    if (_appleGoInBasket3)
    //    {
    //        ApplesGoInBascket(_apple8);
    //        ApplesGoInBascket(_apple9);
    //        ApplesGoInBascket(_apple10);
    //        ApplesGoInBascket(_apple11);
    //        ApplesGoInBascket(_apple12);
    //    }

    //}


    /// <summary>
    /// かごの数字によって、かごの中にあるリンゴの表示が変わる
    /// </summary>
    public void ShowInBasketApples()
    {
        if(Count <= 7)
        {
            // かごのリンゴ数が９個未満の時
            // かごにあるリンゴの画像は表示しない
            _appleInbasket1.SetActive(false);
            _appleInbasket2.SetActive(false);
            _appleInbasket3.SetActive(false);
            _appleInbasket4.SetActive(false);
        }
        if(8 <= Count)
        {
            // かごのリンゴ数が 10個以上 の時
            // かごのリンゴを 1個 表示する
            _appleInbasket1.SetActive(true);
        }
        if (11 <= Count)
        {
            // かごのリンゴ数が 13個以上 の時
            // かごのリンゴを 2個 表示する
            _appleInbasket2.SetActive(true);
        }
        if (14 <= Count)
        {
            // かごのリンゴ数が 15個以上 の時
            // かごのリンゴを 3個 表示する
            _appleInbasket3.SetActive(true);
        }
        if (16 <= Count)
        {
            // かごのリンゴ数が 15個以上 の時
            // かごのリンゴを 6個 表示する
            _appleInbasket4.SetActive(true);
            _appleInbasket5.SetActive(true);
        }
    }

    /// <summary>
    /// リセットボタンを押されたとき
    /// </summary>
    private void Reset()
    {
        // リンゴの数をリセット
        Count = 7;

        // リンゴの木をさわれるように
        _canTouchTree  = true;
        _canTouchTree2 = true;
        _canTouchTree3 = true;

        // リンゴの表示をリセット
        #region
        #endregion
    }

    void kanss(Rigidbody rb, GameObject a)
    {
        rb = a.GetComponent<Rigidbody>();
        rb.isKinematic = false;

    }

    /// <summary>
    /// かごの中にあるリンゴを数え続ける
    /// </summary>
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