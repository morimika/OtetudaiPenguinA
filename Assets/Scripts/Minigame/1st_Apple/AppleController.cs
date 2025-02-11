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
using NaughtyAttributes.Test;

// Matsukawa

public class AppleController : MonoBehaviour
{
    // 消えたAppleの数を数える
    // 取ったAppleの数が15個だったら達成それ以外の数字なら失敗をgameManagerに伝える

    #region インスペクター上
    public static AppleController instance;
    public        TextMeshProUGUI textBasketNum;
    public int                    Count = 7;

    [Foldout("木のpos"), SerializeField] protected Transform _tree1pos;
    [Foldout("木のpos"), SerializeField] protected Transform _tree2pos;
    [Foldout("木のpos"), SerializeField] protected Transform _tree3pos;

    // それぞれの木が持ってるりんごの数をあらかじめ設定
    private int _tree1 = 4;
    private int _tree2 = 3;
    private int _tree3 = 5;

    // それぞれの木についたUntaggedTagのゲームオブジェクト
    [SerializeField]
    private GameObject[] _treeNullTag = new GameObject[3];

    // かごの現在地を取得
    [SerializeField] private Vector3 _basketPos;
    [SerializeField] private Vector3 _basketPos2;   // 上から入っているように見せるためのpos2

    // かごの中にあるリンゴ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket1; // かごのリンゴ 10 以上で表示　リンゴは１つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket2; // かごのリンゴ 13 以上で表示　リンゴは２つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket3; // かごのリンゴ 15 以上で表示　リンゴは３つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket4; // かごのリンゴ 17 以上で表示　リンゴは６つ
    [Foldout("かごの中にあるりんご"), SerializeField] private GameObject _appleInbasket5; // かごのリンゴ 17 以上で表示　リンゴは６つ(レイヤーの関係でふたつある)

    // りんごプレファブ
    // それぞれタグが違う（３種類）
    [SerializeField] GameObject[] _arrayApplePrefab = new GameObject[3];

    //mori
    [SerializeField]
    private GameObject clearCutInObj;
    [SerializeField]
    private GameObject retryCutInObj;
    [SerializeField]
    private TransitonScene _transitonScene;

    #endregion

    // りんご
    private GameObject[] _arrayApple = new GameObject[12];
    // インスタンシエイトしたりんごプレファブのRigidbody
    private Rigidbody[] _arrayAppleRb = new Rigidbody[12];
    // インスタンシエイトしたりんごプレファブのtransformPosition
    private Transform[] _arrayApplePos = new Transform[12];
    // インスタンシエイトしたりんごプレファブのCollider
    private Collider[] _arrayAppleCol = new Collider[12];

    // はさみが木に触れていいかどうか
    private bool _blCanTouchTree = true;
    private bool _blCanTouchTree2 = true;
    private bool _blCanTouchTree3 = true;

    // リンゴがかごに移動する速さ
    //mori
    [SerializeField]
    protected float _appleAnimation = 3.0f;
    [SerializeField]
    protected float _waitAppleAnimation = 2;     // 上のアニメーションが始まるまで待つ時間

    // はさみのカットアニメーション
    private Animator _scissorCuttingAnimation;
    private float _scissorAnimWait = 1.0f;

    // クリア判定
    // クリア判定の関数は OnClickClearButton() 250行
    public bool _blClear = false;

    //mori
    public bool _isStart = false;
    public bool _isRestart = false;
    private GameObject _go;
    private bool _doOnce = false;
    private bool priClear = false;


    public void Start()
    {

        CountBasketAppleNum();

        GenerateApple();
        GenerateApple2();
        GenerateApple3();

        _scissorCuttingAnimation = this.gameObject.GetComponent<Animator>();
        _scissorCuttingAnimation.GetComponent<Animator>().enabled = false;
        _transitonScene = GetComponent<TransitonScene>();
    }

    #region    りんごをインスタンス化

    public void GenerateApple()
    {
        // 木１のりんごを生成
        _arrayApple[0] = Instantiate(_arrayApplePrefab[0], new Vector3(-5.7f, 0.6f, 0), Quaternion.identity, _tree1pos) as GameObject;
        _arrayApple[1] = Instantiate(_arrayApplePrefab[0], new Vector3(-3.3f, 0.6f, 0), Quaternion.identity, _tree1pos) as GameObject;
        _arrayApple[2] = Instantiate(_arrayApplePrefab[0], new Vector3(-5.1f, 2f, 0), Quaternion.identity, _tree1pos) as GameObject;
        _arrayApple[3] = Instantiate(_arrayApplePrefab[0], new Vector3(-3.9f, 2f, 0), Quaternion.identity, _tree1pos) as GameObject;

        // apple　GetComponents
        _arrayAppleRb[0] = _arrayApple[0].GetComponent<Rigidbody>();
        _arrayAppleRb[1] = _arrayApple[1].GetComponent<Rigidbody>();
        _arrayAppleRb[2] = _arrayApple[2].GetComponent<Rigidbody>();
        _arrayAppleRb[3] = _arrayApple[3].GetComponent<Rigidbody>();

        _arrayApplePos[0] = _arrayApple[0].GetComponent<Transform>();
        _arrayApplePos[1] = _arrayApple[1].GetComponent<Transform>();
        _arrayApplePos[2] = _arrayApple[2].GetComponent<Transform>();
        _arrayApplePos[3] = _arrayApple[3].GetComponent<Transform>();

        _arrayAppleCol[0] = _arrayApple[0].GetComponent<Collider>();
        _arrayAppleCol[1] = _arrayApple[1].GetComponent<Collider>();
        _arrayAppleCol[2] = _arrayApple[2].GetComponent<Collider>();
        _arrayAppleCol[3] = _arrayApple[3].GetComponent<Collider>();

        _treeNullTag[0].SetActive(true);
    }

    public void GenerateApple2()
    {
        // 木２
        _arrayApple[4] = Instantiate(_arrayApplePrefab[1], new Vector3(-0.8f, -1.1f, 0), Quaternion.identity, _tree2pos) as GameObject;
        _arrayApple[5] = Instantiate(_arrayApplePrefab[1], new Vector3(0.8f, -1.1f, 0), Quaternion.identity, _tree2pos) as GameObject;
        _arrayApple[6] = Instantiate(_arrayApplePrefab[1], new Vector3(0, 0.5f, 0), Quaternion.identity, _tree2pos) as GameObject;

        // apple　GetComponents
        _arrayAppleRb[4] = _arrayApple[4].GetComponent<Rigidbody>();
        _arrayAppleRb[5] = _arrayApple[5].GetComponent<Rigidbody>();
        _arrayAppleRb[6] = _arrayApple[6].GetComponent<Rigidbody>();

        _arrayApplePos[4] = _arrayApple[4].GetComponent<Transform>();
        _arrayApplePos[5] = _arrayApple[5].GetComponent<Transform>();
        _arrayApplePos[6] = _arrayApple[6].GetComponent<Transform>();

        _arrayAppleCol[4] = _arrayApple[4].GetComponent<Collider>();
        _arrayAppleCol[5] = _arrayApple[5].GetComponent<Collider>();
        _arrayAppleCol[6] = _arrayApple[6].GetComponent<Collider>();

        _treeNullTag[1].SetActive(true);
    }
    public void GenerateApple3()
    {
        // 木３
        _arrayApple[7] = Instantiate(_arrayApplePrefab[2], new Vector3(3.3f, 0.6f, 0), Quaternion.identity, _tree3pos) as GameObject;
        _arrayApple[8] = Instantiate(_arrayApplePrefab[2], new Vector3(5.7f, 0.6f, 0), Quaternion.identity, _tree3pos) as GameObject;
        _arrayApple[9] = Instantiate(_arrayApplePrefab[2], new Vector3(3.9f, 2f, 0), Quaternion.identity, _tree3pos) as GameObject;
        _arrayApple[10] = Instantiate(_arrayApplePrefab[2], new Vector3(5.1f, 2f, 0), Quaternion.identity, _tree3pos) as GameObject;
        _arrayApple[11] = Instantiate(_arrayApplePrefab[2], new Vector3(4.5f, 0.4f, 0), Quaternion.identity, _tree3pos) as GameObject;

        // apple　GetComponents
        _arrayAppleRb[7] = _arrayApple[7].GetComponent<Rigidbody>();
        _arrayAppleRb[8] = _arrayApple[8].GetComponent<Rigidbody>();
        _arrayAppleRb[9] = _arrayApple[9].GetComponent<Rigidbody>();
        _arrayAppleRb[10] = _arrayApple[10].GetComponent<Rigidbody>();
        _arrayAppleRb[11] = _arrayApple[11].GetComponent<Rigidbody>();

        _arrayApplePos[7] = _arrayApple[7].GetComponent<Transform>();
        _arrayApplePos[8] = _arrayApple[8].GetComponent<Transform>();
        _arrayApplePos[9] = _arrayApple[9].GetComponent<Transform>();
        _arrayApplePos[10] = _arrayApple[10].GetComponent<Transform>();
        _arrayApplePos[11] = _arrayApple[11].GetComponent<Transform>();

        _arrayAppleCol[7] = _arrayApple[7].GetComponent<Collider>();
        _arrayAppleCol[8] = _arrayApple[8].GetComponent<Collider>();
        _arrayAppleCol[9] = _arrayApple[9].GetComponent<Collider>();
        _arrayAppleCol[10] = _arrayApple[10].GetComponent<Collider>();
        _arrayAppleCol[11] = _arrayApple[11].GetComponent<Collider>();

        _treeNullTag[2].SetActive(true);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        //mori
        //失敗
        if (Count > 15 && _isRestart==false)
        {
            _isRestart = true;
            _go=Instantiate(retryCutInObj);
            return;
        }
        // Debug.Log(HelpManager.IsClear);
        //ボタンを押したとき
        if (Input.GetMouseButtonDown(0))
        {
            //クリアしている、かつ、フェードインされて待機中の場合
            if (priClear == true && CutInFade.IsFadeFin)
            {
                Invoke(nameof(ReturnGameScene), 1);
            }
        }
        //クリア後は動かさない
        if (HelpManager.IsClear == true) return;

        ClearJudge();

        // かごの中にある数を更新する
        CountBasketAppleNum();
        // 取得しているりんごの数によってかごの中にあるリンゴの画像が変わる
        ShowInBasketApples();
    }

    // はさみが木に触れたときりんごがおちる
    public async void OnCollisionEnter2D(Collision2D collision)
    {

        //mori
        if (_blClear) return;

        if (collision.gameObject.name == "Tree1" &&  _blCanTouchTree == true)
        {
            // はさみがチョキチョキするアニメーション開始
            _scissorCuttingAnimation.enabled = true;
            _scissorCuttingAnimation.SetBool("_isCutting", true);

            // 木１に当たった時
            // 木１のリンゴの数とかごにある数字を足す
            Count = Count + _tree1;

            // りんごを落とす
            _arrayAppleRb[0].isKinematic = false;
            _arrayAppleRb[1].isKinematic = false;
            _arrayAppleRb[2].isKinematic = false;
            _arrayAppleRb[3].isKinematic = false;

            // 一度リンゴが落ちた木はもう一度触れない
            _blCanTouchTree = false;
            _treeNullTag[0].SetActive(false);

            //// かごに入るアニメーション
            await UniTask.Delay(TimeSpan.FromSeconds(_waitAppleAnimation));
            ApplesGoInBascket(_arrayApplePos[0], _basketPos, _basketPos2, _arrayAppleCol[0], _arrayAppleRb[0], _arrayApple[0].gameObject);
            ApplesGoInBascket(_arrayApplePos[1], _basketPos, _basketPos2, _arrayAppleCol[1], _arrayAppleRb[1], _arrayApple[1].gameObject);
            ApplesGoInBascket(_arrayApplePos[2], _basketPos, _basketPos2, _arrayAppleCol[2], _arrayAppleRb[2], _arrayApple[2].gameObject);
            ApplesGoInBascket(_arrayApplePos[3], _basketPos, _basketPos2, _arrayAppleCol[3], _arrayAppleRb[3], _arrayApple[3].gameObject);

            // はさみのアニメーションの時間が過ぎたらアニメーションをおわらせる
            await UniTask.Delay(TimeSpan.FromSeconds(_scissorAnimWait));
            // またはさみのアニメーションが呼び出せるように _isCutting を falseにする
            _scissorCuttingAnimation.SetBool("_isCutting", false);
        }
        else if (collision.gameObject.tag == "Tree2" && _blCanTouchTree2 == true)
        {
            _scissorCuttingAnimation.enabled = true;
            _scissorCuttingAnimation.SetBool("_isCutting", true);

            Count = Count + _tree2;

            _arrayAppleRb[4].isKinematic = false;
            _arrayAppleRb[5].isKinematic = false;
            _arrayAppleRb[6].isKinematic = false;

            _blCanTouchTree2 = false;
            _treeNullTag[1].SetActive(false);

            await UniTask.Delay(TimeSpan.FromSeconds(_waitAppleAnimation));
            ApplesGoInBascket(_arrayApplePos[4], _basketPos, _basketPos2, _arrayAppleCol[4], _arrayAppleRb[4], _arrayApple[4].gameObject);
            ApplesGoInBascket(_arrayApplePos[5], _basketPos, _basketPos2, _arrayAppleCol[5], _arrayAppleRb[5], _arrayApple[5].gameObject);
            ApplesGoInBascket(_arrayApplePos[6], _basketPos, _basketPos2, _arrayAppleCol[6], _arrayAppleRb[6], _arrayApple[6].gameObject);

            await UniTask.Delay(TimeSpan.FromSeconds(_scissorAnimWait));
            _scissorCuttingAnimation.SetBool("_isCutting", false);
        }
        else if (collision.gameObject.tag == "Tree3" && _blCanTouchTree3 == true)
        {
            _scissorCuttingAnimation.enabled = true;
            _scissorCuttingAnimation.SetBool("_isCutting", true);

            Count = Count + _tree3;

            _arrayAppleRb[7].isKinematic = false;
            _arrayAppleRb[8].isKinematic = false;
            _arrayAppleRb[9].isKinematic = false;
            _arrayAppleRb[10].isKinematic = false;
            _arrayAppleRb[11].isKinematic = false;

            _blCanTouchTree3 = false;
            _treeNullTag[2].SetActive(false);

            await UniTask.Delay(TimeSpan.FromSeconds(_waitAppleAnimation));
            ApplesGoInBascket(_arrayApplePos[7], _basketPos, _basketPos2, _arrayAppleCol[7], _arrayAppleRb[7], _arrayApple[7].gameObject);
            ApplesGoInBascket(_arrayApplePos[8], _basketPos, _basketPos2, _arrayAppleCol[8], _arrayAppleRb[8], _arrayApple[8].gameObject);
            ApplesGoInBascket(_arrayApplePos[9], _basketPos, _basketPos2, _arrayAppleCol[9], _arrayAppleRb[9], _arrayApple[9].gameObject);
            ApplesGoInBascket(_arrayApplePos[10], _basketPos, _basketPos2, _arrayAppleCol[10], _arrayAppleRb[10], _arrayApple[10].gameObject);
            ApplesGoInBascket(_arrayApplePos[11], _basketPos, _basketPos2, _arrayAppleCol[11], _arrayAppleRb[11], _arrayApple[11].gameObject);

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
            Destroy(obj);
        }

        //mori
        //0.5秒まってクリアする
        await Task.Delay(500);
        if (Count == 15)
        {
            _blClear = true;
            //マップに戻ったときNPCの前にプレイヤーを配置する
            PlayerSetPos.PlayerPos = new Vector2(1.3f, 3.7f);
        }
    }

    //mori
    //シーン遷移
    public void ReturnGameScene()
    {
        _transitonScene?.LoadScene();
    }

    public void ClearJudge()
    {
        // 15かどうか確認
        // 15なら _blClear を trueにする
        // 15以外ならリセット

        //mori
        //クリアしているなら
        if (_blClear == true && !_doOnce)
        {
            _doOnce = true;
            //カットインを呼び、クリアにする
            Instantiate(clearCutInObj);
            priClear = true;
            if (HelpManager.HavingHelpTask== "AppleCut")
            {
                HelpManager.IsClear = true;
            }
            Debug.Log("クリア");
        }

    }


    // リセットボタンを押されたとき
    public void OnClickResetButton()
    {
        _isRestart = false;
        Destroy(_go.gameObject);
        // はさみを初期位置に戻す
        ScissorToInitialPos();
        //// リンゴの数をリセット
        Count = 7;

        if (_blCanTouchTree == false)
        {
            if (Tree1._blcanGenerateApple == true)
            {
                //// 木をまた触れるようにする
                _blCanTouchTree = true;
                GenerateApple();
            }
        }

        if (_blCanTouchTree2 == false)
        {
            if (Tree2._blcanGenerateApple2 == true)
            {
                //// 木をまた触れるようにする
                _blCanTouchTree2 = true;
                GenerateApple2();
            }
        }
        if (_blCanTouchTree3 == false)
        {
            if(Tree3._blcanGenerateApple3 == true)
            {
                //// 木をまた触れるようにする
                _blCanTouchTree3 = true;
                GenerateApple3();
            }
        }
    }

    /// <summary>
    /// はさみを初期位置に戻すアニメーション
    /// </summary>
    public Vector3 ScissorToInitialPos()
    {
        // はさみのアニメーションをストップ
        _scissorCuttingAnimation.SetBool("_isCutting", false);

        Vector3 _scissorInitialPos = new Vector3(3f, -3.5f, 0f);

        Vector3 recentPos = this.transform.position;
        if (recentPos != _scissorInitialPos)
        {
            // this.transform.DOMove(_scissorInitialPos, 0.2f); 
            this.transform.position = _scissorInitialPos;
            return _scissorInitialPos;
        }
        else if(recentPos ==  _scissorInitialPos)
        {
            this.transform.position = _scissorInitialPos;
            return _scissorInitialPos;
        }
        else
        {
            return Vector3.zero;
        }
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
            _appleInbasket5.SetActive(false);
        }
        else if (10 <= Count && 12 >= Count)
        {
            // かごのリンゴ数が 10個以上 の時
            // かごのリンゴを 1個 表示する
            _appleInbasket1.SetActive(true);
        }
        else if (13 <= Count && 14 >= Count)
        {
            // かごのリンゴ数が 13個以上 の時
            // かごのリンゴを 2個 表示する
            _appleInbasket1.SetActive(true);
            _appleInbasket2.SetActive(true);
        }
        else if (15 <= Count && 16 >= Count)
        {
            // かごのリンゴ数が 15個以上 の時
            // かごのリンゴを 3個 表示する
            _appleInbasket1.SetActive(true);
            _appleInbasket2.SetActive(true);
            _appleInbasket3.SetActive(true);
        }
        else if (17 <= Count && 19 >= Count)
        {
            // かごのリンゴ数が 15個以上 の時
            // かごのリンゴを 6個 表示する
            _appleInbasket1.SetActive(true);
            _appleInbasket2.SetActive(true);
            _appleInbasket3.SetActive(true);
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
        if (!_isStart) return;
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (!_isStart) return;
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