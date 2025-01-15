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
    
    
    //タップした回数
    private int tap = 0;

    //アニメーション管理用
    private bool sprite_C = false;

    //移動中かどうかの判定
    private bool move = false;

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
                //クリックSEを鳴らす
                BSJSoundManger.Instance.PlaySE(0);
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
                    move = true;
                   
                    Debug.Log($"tap Count:{tap}");
                    //タップした間隔を見る
                    Invoke("Tap", _tapInterval);
                    
                   
                }
            }
            
        }

        //移動中なら
        if (move)
        {
            
            //走っていない時
            if (!run)
            {
                //通常の移動速度をかける
                transform.position = Vector3.MoveTowards(transform.position, target_Point, MoovSpeed * Time.deltaTime);
                //処理は一回だけ実行させるようにする
                if (!sprite_C)
                {
                    Move_Direction(run);

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
                    Move_Direction(run);
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
            //歩く足音
            BSJSoundManger.Instance.PlaySE(1);
            return;
        }

        //ダッシュ状態にするかどうか
        if(tap == 2)
        {
            run = true;
            sprite_C = false;
            tap = 0;
            //走るSE
            BSJSoundManger.Instance.PlaySE(2);
            return;
        }
        //連打したら無視する
        if(tap <= 3)
        {
            run = false;
            tap = 0;
            return;
        }
        
    }

    //アニメーションの管理用
    void Move_Direction(bool flag)
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
                    //歩き
                    if (backAnime != "Up" || backAnime == null)
                    {
                        backAnime = "Up";
                        _animator.SetTrigger("Up");
                    }
                }
                //走り
                else
                {
                    boxCol2D.size = new Vector2(1.4f, 1);
                    if (backAnime != "Run_Back" || backAnime == null)
                    {
                       backAnime = "Run_Back";
                        _animator.SetTrigger("Run_Back");
                       
                    }
                    
                }
            }
            //下方向
            else
            {
                if (!flag)
                {
                    if (backAnime != "Down" || backAnime == null)
                    {
                        backAnime = "Down";
                        _animator.SetTrigger("Down");
                   
                    }
                }
                else
                {
                    boxCol2D.size = new Vector2(1.4f, 1);
                    if (backAnime != "Run_Foward" || backAnime == null)
                    {
                        backAnime = "Run_Foward";
                        _animator.SetTrigger("Run_Foward");
                        
                    }
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

                    if (backAnime != "Right" || backAnime == null)
                    {
                        backAnime = "Right";
                        _animator.SetTrigger("Right");
                    }
                }
                else
                {
                    boxCol2D.size = new Vector2(1.8f, 1.1f);
                    if (backAnime != "Run_Right" || backAnime == null)
                    {
                        backAnime = "Run_Right";
                        _animator.SetTrigger("Run_Right");
                    }
                }
            }
            //左方向
            else
            {
                if (!flag)
                {
                    if (backAnime != "Left" || backAnime == null)
                    {
                        backAnime = "Left";
                        _animator.SetTrigger("Left");
                    }
                }
                else
                {
                    boxCol2D.size = new Vector2(1.8f, 1.1f);
                    if (backAnime != "Run_Left" || backAnime == null)
                    {
                        backAnime = "Run_Left";
                        _animator.SetTrigger("Run_Left");
                    }
                }
            }
        }
    }
    
    
    
    //移動後の初期化
    public void Moov_Finish()
    {
        
        
        //初期化
        run = false;
        move = false;
        sprite_C = false;
        
        _animator.SetTrigger("Idle");
        
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

   
   
}