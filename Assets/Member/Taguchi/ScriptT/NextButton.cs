using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NextButton : MonoBehaviour
{
    [SerializeField] public GameObject ActionCamera;
    [SerializeField] public GameObject Mondai;


    Vector3 CameraTransform;//時計を真ん中にする座標を保存しておく場所

    //Bool関数
    public bool GoCamera = false;//カメラを移動させるために使うbool関数
    public bool Answer = false;
    public bool CheckStart = false;//ボタンを押されたら時計についているスクリプトに次に行く信号を出す

    public GameObject MondaiPanel;//問題を表示しているゲームオブジェクトを入れる
    public GameObject MondaiBun;//問題を表示しているゲームオブジェクトを入れる
    public GameObject BG;

    ///////////
    //アニメーション用
    private bool CStartSin = false;
    //一回だけ呼び出すためのやーつ
    private bool OnryCheck = false;


    ///////////


    public float DeleteTime = 0.5f;
    void Start()
    {
        CameraTransform = new Vector3(0, 0, -10);
        CStartSin = true;
        OnryCheck = false;
    }
    //            StartCoroutine(ChangePaneltoMiniSize());
    // Update is called once per frame
    void Update()
    {
        CStart();
        if (OnryCheck == false)
        {
            Debug.Log("BBBBBB");
            if (this.transform.localScale.x >= 0.8f )//|| transform.localScale.y >= 0.8f || transform.localScale.z >= 0.8f)
            {
                Debug.Log("aaaa");
                CStartSin = false;
               
              /*
                if (transform.localScale.x <= 0.7 || transform.localScale.y <= 0.7 || transform.localScale.z <= 0.7)
                {
                    OnryCheck = true;
                    CStartSin = true;
                }
              */
            }
        }
       

    }
    public void Anser()
    {
        
        GoCamera = true;//別スクリプトで使う
        CheckStart = true;
       

       
        // Destroy(this.gameObject,DeleteTime);//指定した時間後にこのオブジェクトを削除する
    }
    IEnumerator ChangePaneltoBigSize()
    {
        var size = 0f;
        var speed = 0.02f;
       

        while (size <= 1.0f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(2, 2, 1), size);
            size += speed;

            yield return null;
        }
    }
    IEnumerator ChangePaneltoMiniSize()
    {
        var size = 0f;
        var speed = 0.02f;


        while (size >= 0.7f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(3, 3, 2), size);
            size += speed;

            yield return null;
        }
    }
    private void CStart()
    {
      /*  Invoke("RStop",0.3f);
        Invoke("RStart", 0.5f);
      */
        if(CStartSin == true)
        {
            StartCoroutine(ChangePaneltoBigSize());//大きくする
        }
        else if (CStartSin == false)
        {
          //  StartCoroutine(ChangePaneltoMiniSize());//小さくする

        }
       

    }
   private void RStart()
    {
        CStartSin = true;
    }
    private void RStop()
    {
        CStartSin = false;
    }
   

}
/*
            this.transform.localScale = new Vector3(i, i, i);

  
    }*/