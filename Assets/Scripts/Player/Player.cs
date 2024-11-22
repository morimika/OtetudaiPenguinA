using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    ////[SerializeField] GameManager GM  
    //ルール変更可能
    [SerializeField, Header("移動速度")] private float MoovSpeed;
    [SerializeField, Header("走る速度")] private float RunSpeed;
    [SerializeField, Header("ダブルタップの間隔")] private float _tapInterval;

    [SerializeField, Header(("現在のタイプなのか"))]
    private PlaySceneDatas _playSceneDatas;

    //mori
    [SerializeField,Label("タップ地点に生成するアイコン")]
    private GameObject _tapPointObj;
    private GameObject _tapPointSaver;

    //タップした回数
    private int tap = 0;

    //アニメーション管理用
    private bool sprite_C = false;

    //移動中かどうかの判定
    private bool moov = false;

    //走っているかどうか
    private bool run = false;
    
    private string backAnime;

    //移動先の取得用
    private Vector3 target_Point;

    //コライダー取得用
    private BoxCollider2D boxCol2D;

    //オブジェクトがタップ先にあるかどうかの判定
    private GameObject clickedObject;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        //初期はPlayerに設定しておく
        _playSceneDatas.TapType = PlaySceneTapType.Play;

        //コライダー取得
        boxCol2D = GetComponent<BoxCollider2D>();
        //アニメーター取得
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイ中なら処理を呼び出す
        if (_playSceneDatas.TapType == PlaySceneTapType.Play)
        {
            //押したとき
            if (Input.GetMouseButtonDown(0))
            {
                //ボタン系を押さないときは全て処理しない
                if(EventSystem.current.IsPointerOverGameObject()) return;
                
                //スクリーン座標からワールド座標に変換
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //レイを飛ばす
                RaycastHit2D hit2d = Physics2D.Raycast(worldPoint, Vector2.zero);

                //当たり判定があった場合の処理z
                if (hit2d)
                {
                }
                else
                {
                    //スクリーン座標からワールド座標に変換
                    target_Point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //カメラの奥行き
                    target_Point.z += 10;
                    sprite_C = false;
                    //タップを加算
                    tap++;
                    moov = true;
                    //タップした間隔を見る
                    Invoke("Tap", _tapInterval);
                    //mori
                    if(_tapPointSaver!=null)Destroy(_tapPointSaver);
                    _tapPointSaver=Instantiate(_tapPointObj, target_Point, Quaternion.identity);
                }
            }
        }

        //移動中なら
        if (moov)
        {
            //走っていない時
            if (!run)
            {
                //通常の移動速度をかける
                transform.position = Vector3.MoveTowards(transform.position, target_Point, MoovSpeed * Time.deltaTime);
                //処理は一回だけ実行させるようにする
                if (!sprite_C)
                {
                    Moov_Direction(run);
                    //走る時にコライダーを再設定する
                    boxCol2D.size = new Vector2(1, 1.4f);
                }
            }
            //走っている時
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target_Point, RunSpeed * Time.deltaTime);
                //処理は一回だけ実行させるようにする
                if (!sprite_C)
                {
                    Moov_Direction(run);
                }
            }

            //移動先に着いたら初期化
            if (transform.position == target_Point)
            {
                Moov_Finish();
            }
        }
    }

    //ダブルタップの処理
    void Tap()
    {
        //ダブルタッチされているか
        if (tap == 1)
        {
            tap = 0;
            return;
        }

        //ダッシュ状態にするかどうか
        else
        {
            run = true;
            sprite_C = false;
            tap = 0;
            return;
        }
    }

    //アニメーションの管理用
    void Moov_Direction(bool flag)
    {
           sprite_C = true;
        //どの方向に進んでいるか
        Vector3 dis = transform.position - target_Point;
        //絶対値を計算
        float posX = Mathf.Abs(dis.x);
        float posY = Mathf.Abs(dis.y);

        //上下の判定
        if (posX < posY)
        {
            //上方向
            if (dis.y < 0.1f)
            {
                if (!flag)
                {
                    _animator.SetTrigger("Up");
                }
                else
                {
                    boxCol2D.size = new Vector2(1.4f, 1);
                }
            }
            //下方向
            else
            {
                if (!flag)
                {
                    _animator.SetTrigger("Down");
                }
                else
                {
                    boxCol2D.size = new Vector2(1.4f, 1);
                }
            }
        }
        //左右方向
        else
        {
            //右方向
            if (dis.x < 0.1f)
            {
                if (!flag)
                {
                    _animator.SetTrigger("Right");
                }
                else
                {
                    boxCol2D.size = new Vector2(1.8f, 1.1f);
                }
            }
            //左方向
            else
            {
                if (!flag)
                {
                    _animator.SetTrigger("Left");
                }
                else
                {
                    boxCol2D.size = new Vector2(1.8f, 1.1f);
                }
            }
        }
    }

    //移動後の初期化
    public void Moov_Finish()
    {
        //初期化
        run = false;
        moov = false;
        sprite_C = false;
        boxCol2D.size = new Vector2(1, 1.4f);
    }

    //最初にぶつかった追突からのガリガリ防止①
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Moov_Finish();
    }

    //オブジェクトにあたっていたときの食い込み防止
    public void OnCollisionStay2D(Collision2D collision)
    {
        Moov_Finish();
    }

    //mori
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _tapPointSaver)
        {
            Destroy(_tapPointSaver);
        }
    }
}