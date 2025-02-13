using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI.Table;

public class CenterCirclController : MonoBehaviour
{
    [SerializeField] private List<GearController> _gearControllers = new List<GearController>();

    public NextButton NextButton;//次に行くボタンの判定を拾うために入れている

    ////////////時計の蓋関連///////////////////////
    public GameObject FlontClock;//時計の開く蓋のパーツ
    public float Openspeed = 5.0f;//蓋の座標移動スピード
    [SerializeField] Transform Openpositiontarget;//開くときに蓋をどの位置に移動させるかのターゲット
    [SerializeField] Transform Closepositiontarget;//閉じるときに蓋をどの位置に移動させるかのターゲット
    private bool Close = true;//蓋を開けるタイミングかどうかを判断するための変数
    bool Rot = true;//蓋を開けるアニメーションの時に使う 
    public float RotateSpeed = 0.1f;//回転速度
    public GameObject FlontBard;//時計の一番上にある鳥の蓋
    /// //////////////////////////////////////////

    ////////時計の長針/////////////
    public GameObject LongClock;//時計の長針を入れるよう
    public int GoodLoMeter = 15;//正解の長針の移動速度用
    public int LongSinCount;
    //////////////////////////////

    /////////ゲームクリアとオーバーのUI用////////////
    public GameObject ClereUI;
    public GameObject ClereEffect;

    public GameObject OverUI;
    public GameObject OverEffect;

    //////////////////////////////
    AudioSource audioSource;

    public AudioClip sound2;//開く音
    public AudioClip sound3;//閉まる音
    private bool CloseSECheck = false;

    bool isCalledOnce = false;//最後の判定の処理を一回だけ呼び出す用

    private int _answer = -1;
    public int answer = 0;

    public bool AnswerCheck = false;//はめたギアが正しいものかどうかを判定する
    public  int Answer => _answer;

    [SerializeField] private string _loadScene; //シーン名を記述

   
   
    private void Start()
    {
        _gearControllers.ForEach(gear => gear.Setup(AttachGearObject));
        if(Rot == true)
        {
            Invoke("LidMove", 3f);
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
       
        //答え合わせを一回だけ呼ぶ
        if (!isCalledOnce)
        {
           
            if (NextButton.CheckStart)
            {
                if (Rot == true)
                {
                    Close = true;
                    Invoke("ComSE", 1f);
                    Rot = false;
                    StartCoroutine(Cl());
                   
                }
                
                Close = true;
                Invoke("Finish", 3f);
            }
        }
        else
        {
        }
           

        Lid();
    }

    //答えがあってるかないかでそれぞれを呼び出すための分岐
    private void Finish()
    {
        isCalledOnce = true;
        Debug.Log("答え合わせが呼び出された");

        if (AnswerCheck == true)
        {
            Debug.Log("金属");
            Invoke("Metal", 0f);

        }
        else
        {

            if (_answer == 2)
            {
                Debug.Log("プラスチック");
                Invoke("LotatePura", 0f);
            }
            else if(_answer == 3)
            {
                Debug.Log("木材");
                Invoke("LotateWood", 0f);
            }
        }
    }

    private void AttachGearObject(int index)
    {
        
        //今ついているギアのインデックスを拾う
        _answer = index;
 　　   //今拾っているインデックスの数値をログに表示
        Debug.Log($"Index: {index}");
        _gearControllers.ForEach(gear => gear.SetGearEnable(false));
        if(_answer == answer)
        {
            AnswerCheck = true;
            Debug.Log("OK");
           
        }
        else
        {
            AnswerCheck = false;
            Debug.Log("No");
            
        }
    }
    
    public void SetFreeMoveStatus()
    {
        _gearControllers.ForEach(gear => gear.SetGearEnable(true));
    }

    private void Lid()
    {
        //時計の蓋を移動させるために呼ぶ
        if (Close == false)
        {
            FlontClock.transform.DOMove(new Vector3(-8.4f, -3.0f, 2f), 2.2f);//数字のある蓋
            FlontClock.transform.DORotate(Vector3.up * -180f, 2.8f);
            FlontBard.transform.DOMove(new Vector3(-8.4f, -2.3f, 5f), 2.0f);//鳥の蓋
            FlontBard.transform.DORotate(Vector3.up * -180f, 2.8f);
        }
        else
        {
            FlontClock.transform.DOMove(new Vector3(-1.3f, -3.3f, -5f), 3f);
            FlontClock.transform.DORotate(Vector3.up * 0f, 3f);
        }
    }

    private void LidMove()
    {
        Close = false;
        Rot = false;
        StartCoroutine(rt());
        Invoke("OpenSE", 3f);
    }
    //蓋を開ける回転のコルーチン
    IEnumerator rt()
    {
        int i = 0;
        while (i > -330)
        {
            i--;
            this.transform.Rotate(0, -RotateSpeed, 0);
            yield return null;
        }
        Rot = true;
    }
    //蓋を閉める回転のコルーチン

    IEnumerator Cl()
    {
        int i = 0;
        while (i < 0.02)
        {
            i++;
            this.transform.Rotate(0, RotateSpeed, 0);
            yield return null;
        }
        Rot = true;
    }

    //木のギアを入れたとき
    private void LotateWood()
    {
       
        //回転処理
        if (true)
        {
            LongSinCount += 1;
            if (LongSinCount % 40 == 0)
            {
                LongClock.GetComponent<Transform>().localEulerAngles += new Vector3(0, 0, -5);

                GetComponent<AudioSource>().Play();
            }
            
        }

        Invoke("BadEffect", 3f);
        Invoke("GameOver", 5f);
    }

    //プラスチックのギアを入れたとき
    private void LotatePura()
    {
       
        //回転処理
        if (true)
        {
            LongSinCount += 1;
            if (LongSinCount % 40 == 0)
            {
                LongClock.GetComponent<Transform>().localEulerAngles += new Vector3(0, 0, 15);

                GetComponent<AudioSource>().Play();
            }
        }
        Invoke("BadEffect", 3f);
        Invoke("GameOver", 5f);
    }

    //鉄のギアを入れたとき
    private void Metal()
    {
        
        //回転処理
        if (true)
        {
            LongSinCount += 1;
            if (LongSinCount %40 == 0)
            {
                LongClock.GetComponent<Transform>().localEulerAngles += new Vector3(0, 0, -15);

                GetComponent<AudioSource>().Play();
            }
        }
        Invoke("GoodEffect", 3f);
        Invoke("Clere", 5f);
    }

    private void Clere()
    {
        ClereUI.SetActive(true);
        Invoke("BackStage",1.3f);
    }
    private void GameOver()
    {
        OverUI.SetActive(true);
    }

    private void BackStage()
    {
        SceneManager.LoadScene(_loadScene);
    }
    private void GoodEffect()
    {
        ClereEffect.SetActive(true);
    }
    private void BadEffect()
    {
       OverEffect.SetActive(true);
    }
    private void OpenSE()
    {
        audioSource.PlayOneShot(sound2);
    }
    private void CloseSE()
    {
        audioSource.PlayOneShot(sound3);
    }
    private void ComSE()
    {
     

        if ( CloseSECheck == false)
        {
            CloseSECheck = true;
            Invoke("CloseSE", 2f);
        }
    }
}
