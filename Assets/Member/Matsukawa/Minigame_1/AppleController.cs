using NaughtyAttributes;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

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

    [Foldout("木のpos"), SerializeField] protected Transform _tree1pos;
    [Foldout("木のpos"), SerializeField] protected Transform _tree2pos;
    [Foldout("木のpos"), SerializeField] protected Transform _tree3pos;

    // それぞれの木が持ってるりんごの数をあらかじめ設定
    private int _tree1 = 4;
    private int _tree2 = 3;
    private int _tree3 = 5;

    // かごの現在地を取得
    [SerializeField] private Vector3 _basketPos;
    [SerializeField] private Vector3 _basketPos2;   // 上から入っているように見せるためのpos2

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
    protected float _appleAnimation = 3.0f;
    protected int _waitAppleAnimation = 2;     // 上のアニメーションが始まるまで待つ時間

    // はさみのカットアニメーション
    private Animator _scissorCuttingAnimation;
    private float _scissorAnimWait = 1.0f;

    // クリア判定
    // クリア判定の関数は OnClickClearButton() 250行
    public static bool _blClear = false;

    public void Start()
    {
        CountBasketAppleNum();

        _scissorCuttingAnimation = gameObject.GetComponent<Animator>();
        _scissorCuttingAnimation.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ClearJudge();

        // かごの中にある数を更新する
        CountBasketAppleNum();
        // 取得しているりんごの数によってかごの中にあるリンゴの画像が変わる
        ShowInBasketApples();

    }

    // はさみが木に触れたときりんごがおちる
    public async void OnCollisionEnter2D(Collision2D collision)
    {
        #region apple　GetComponents
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
        Rigidbody  apple12rb = _apple12.GetComponent<Rigidbody>();

        Transform _apple1Pos = _apple1.GetComponent<Transform>();
        Transform _apple2Pos = _apple2.GetComponent<Transform>();
        Transform _apple3Pos = _apple3.GetComponent<Transform>();
        Transform _apple4Pos = _apple4.GetComponent<Transform>();
        Transform _apple5Pos = _apple5.GetComponent<Transform>();
        Transform _apple6Pos = _apple6.GetComponent<Transform>();
        Transform _apple7Pos = _apple7.GetComponent<Transform>();
        Transform _apple8Pos = _apple8.GetComponent<Transform>();
        Transform _apple9Pos = _apple9.GetComponent<Transform>();
        Transform _apple10Pos = _apple10.GetComponent<Transform>();
        Transform _apple11Pos = _apple11.GetComponent<Transform>();
        Transform _apple12Pos = _apple12.GetComponent<Transform>();

        _apple1.transform.position = _apple1Pos.transform.position;
        _apple2.transform.position = _apple2Pos.transform.position;
        _apple3.transform.position = _apple3Pos.transform.position;
        _apple4.transform.position = _apple4Pos.transform.position;
        _apple5.transform.position = _apple5Pos.transform.position;
        _apple6.transform.position = _apple6Pos.transform.position;
        _apple7.transform.position = _apple7Pos.transform.position;
        _apple8.transform.position = _apple8Pos.transform.position;
        _apple9.transform.position = _apple9Pos.transform.position;
        _apple10.transform.position = _apple10Pos.transform.position;
        _apple11.transform.position = _apple11Pos.transform.position;
        _apple12.transform.position = _apple12Pos.transform.position;

        Collider _apple1Coll = _apple1.GetComponent<Collider>();
        Collider _apple2Coll = _apple2.GetComponent<Collider>();
        Collider _apple3Coll = _apple3.GetComponent<Collider>();
        Collider _apple4Coll = _apple4.GetComponent<Collider>();
        Collider _apple5Coll = _apple5.GetComponent<Collider>();
        Collider _apple6Coll = _apple6.GetComponent<Collider>();
        Collider _apple7Coll = _apple7.GetComponent<Collider>();
        Collider _apple8Coll = _apple8.GetComponent<Collider>();
        Collider _apple9Coll = _apple9.GetComponent<Collider>();
        Collider _apple10Coll = _apple10.GetComponent<Collider>();
        Collider _apple11Coll = _apple11.GetComponent<Collider>();
        Collider _apple12Coll = _apple12.GetComponent<Collider>();

        #endregion

        if (collision.gameObject.name == "Tree1" &&  _canTouchTree == true)
        {
            // はさみがチョキチョキするアニメーション開始
            _scissorCuttingAnimation.enabled = true;
            _scissorCuttingAnimation.SetBool("_isCutting", true);

            // 木１に当たった時
            // 木１のリンゴの数とかごにある数字を足す
            Count = Count + _tree1;

            // りんごを落とす
            apple1rb.isKinematic = false;
            apple2rb.isKinematic = false;
            apple3rb.isKinematic = false;
            apple4rb.isKinematic = false;

            // 一度リンゴが落ちた木はもう一度触れない
            _canTouchTree = false;

            //// かごに入るアニメーション
            await UniTask.Delay(TimeSpan.FromSeconds(_waitAppleAnimation));
            ApplesGoInBascket(_apple1Pos, _basketPos, _basketPos2, _apple1Coll, apple1rb, _apple1);
            ApplesGoInBascket(_apple2Pos, _basketPos, _basketPos2, _apple2Coll, apple2rb, _apple2);
            ApplesGoInBascket(_apple3Pos, _basketPos, _basketPos2, _apple3Coll, apple3rb, _apple3);
            ApplesGoInBascket(_apple4Pos, _basketPos, _basketPos2, _apple4Coll, apple4rb, _apple4);

            // はさみのアニメーションの時間が過ぎたらアニメーションをおわらせる
            await UniTask.Delay(TimeSpan.FromSeconds(_scissorAnimWait));
            // またはさみのアニメーションが呼び出せるように _isCutting を falseにする
            _scissorCuttingAnimation.SetBool("_isCutting", false);

        }
        else if (collision.gameObject.tag == "Tree2" && _canTouchTree2)
        {
            _scissorCuttingAnimation.enabled = true;
            _scissorCuttingAnimation.SetBool("_isCutting", true);

            Count = Count + _tree2;

            apple5rb.isKinematic = false;
            apple6rb.isKinematic = false;
            apple7rb.isKinematic = false;

            _canTouchTree2 = false;

            await UniTask.Delay(TimeSpan.FromSeconds(_waitAppleAnimation));
            ApplesGoInBascket(_apple5Pos, _basketPos, _basketPos2, _apple5Coll, apple5rb, _apple5);
            ApplesGoInBascket(_apple6Pos, _basketPos, _basketPos2, _apple6Coll, apple6rb, _apple6);
            ApplesGoInBascket(_apple7Pos, _basketPos, _basketPos2, _apple7Coll, apple7rb, _apple7);

            await UniTask.Delay(TimeSpan.FromSeconds(_scissorAnimWait));
            _scissorCuttingAnimation.SetBool("_isCutting", false);
        }
        else if (collision.gameObject.tag == "Tree3" && _canTouchTree3)
        {
            _scissorCuttingAnimation.enabled = true;
            _scissorCuttingAnimation.SetBool("_isCutting", true);

            Count = Count + _tree3;

            apple8rb.isKinematic = false;
            apple9rb.isKinematic = false;
            apple10rb.isKinematic = false;
            apple11rb.isKinematic = false;
            apple12rb.isKinematic = false;

            _canTouchTree3 = false;


            await UniTask.Delay(TimeSpan.FromSeconds(_waitAppleAnimation));
            ApplesGoInBascket(_apple8Pos, _basketPos, _basketPos2, _apple8Coll, apple8rb, _apple8);
            ApplesGoInBascket(_apple9Pos, _basketPos, _basketPos2, _apple9Coll, apple9rb, _apple9);
            ApplesGoInBascket(_apple10Pos, _basketPos, _basketPos2, _apple10Coll, apple10rb, _apple10);
            ApplesGoInBascket(_apple11Pos, _basketPos, _basketPos2, _apple11Coll, apple11rb, _apple11);
            ApplesGoInBascket(_apple12Pos, _basketPos, _basketPos2, _apple12Coll, apple12rb, _apple12);

            await UniTask.Delay(TimeSpan.FromSeconds(_scissorAnimWait));
            _scissorCuttingAnimation.SetBool("_isCutting", false);
        }
    }

    public async void ApplesGoInBascket(Transform currentPos, Vector3 targetPos, Vector3 targetPos2, Collider coll, Rigidbody rb, GameObject obj)
    {
        // りんご同士で衝突させない（爆発しちゃうから）
        coll.isTrigger = true;
        rb.isKinematic = true;

        // 目的位置(currentPos)に向かって 目的秒(_appleAnimation) かけて移動させる

        await (currentPos.DOMove(targetPos, _appleAnimation));

        if(obj  != null)
        {
           obj.SetActive(false);
        }
    }

    public void ClearJudge()
    {
        // 15かどうか確認
        // 15なら _blClear を trueにする
        // 15以外ならリセット
        if (Count == 15)
        {
            _blClear = true;
            Debug.Log("クリア");
        }

    }


    // リセットボタンを押されたとき
    public void OnClickResetButton()
    {
        SceneManager.LoadScene("Mori_MainGameScene");

        //if (Tree1._blcanGenerateApple1 == true)
        //{
        //    Instantiate(_apple1, new Vector3(-5.7f, 0.6f, 0), Quaternion.identity, _tree1pos).SetActive(true);
        //    Instantiate(_apple2, new Vector3(-3.3f, 0.6f, 0), Quaternion.identity, _tree1pos).SetActive(true);
        //    Instantiate(_apple3, new Vector3(-5.1f, 2f, 0), Quaternion.identity, _tree1pos).SetActive(true);
        //    Instantiate(_apple4, new Vector3(-3.9f, 2f, 0), Quaternion.identity, _tree1pos).SetActive(true);

        //    // 木をまた触れるようにする
        //    _canTouchTree = true;
        //    //// リンゴの数をリセット
        //    Count = 7;
        //    Debug.Log("木１リセット");
        //}

        //if (Tree2._blcanGenerateApple2 == true)
        //{
        //    Instantiate(_apple5, new Vector3(-0.8f, -1.1f, 0), Quaternion.identity, _tree2pos).SetActive(true);
        //    Instantiate(_apple6, new Vector3(0.8f, -1.1f, 0), Quaternion.identity, _tree2pos).SetActive(true);
        //    Instantiate(_apple7, new Vector3(0, 0.5f, 0), Quaternion.identity, _tree2pos).SetActive(true);

        //    _canTouchTree2 = true;
        //    Count = 7;
        //    Debug.Log("木２リセット");
        //}

        //if ( Tree3._blcanGenerateApple3 == true)
        //{
        //    Instantiate(_apple8, new Vector3(3.3f, 0.6f, 0), Quaternion.identity, _tree3pos).SetActive(true);
        //    Instantiate(_apple9, new Vector3(5.7f, 0.6f, 0), Quaternion.identity, _tree3pos).SetActive(true);
        //    Instantiate(_apple10, new Vector3(3.9f, 2f, 0), Quaternion.identity, _tree3pos).SetActive(true);
        //    Instantiate(_apple11, new Vector3(5.1f, 2f, 0), Quaternion.identity, _tree3pos).SetActive(true);
        //    Instantiate(_apple12, new Vector3(4.5f, 0.4f, 0), Quaternion.identity, _tree3pos).SetActive(true);

        //    _canTouchTree3 = true;
        //    Count = 7;
        //    Debug.Log("木２リセット");
        //}
    }

    #region かごの数字によって、かごの中にあるリンゴの表示が変わる
    public void ShowInBasketApples()
    {
        if (Count <= 7)
        {
            // かごのリンゴ数が９個未満の時
            // かごにあるリンゴの画像は表示しない
            _appleInbasket1.SetActive(false);
            _appleInbasket2.SetActive(false);
            _appleInbasket3.SetActive(false);
            _appleInbasket4.SetActive(false);
        }
        if (8 <= Count)
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
    #endregion

    #region りんごの数表示
    public void CountBasketAppleNum()
    {
        // appleの総数表示
        textBasketNum.text = Count.ToString("0");
        Debug.Log(Count);
    }
    #endregion

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