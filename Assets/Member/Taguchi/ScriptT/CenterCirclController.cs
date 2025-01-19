using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    /// //////////////////////////////////////////
    bool isCalledOnce = false;//最後の判定の処理を一回だけ呼び出す用

    private int _answer = -1;
    public int answer = 0;

    public bool AnswerCheck = false;//はめたギアが正しいものかどうかを判定する
    public  int Answer => _answer;

    private void Start()
    {
        _gearControllers.ForEach(gear => gear.Setup(AttachGearObject));
        if(Rot == true)
        {
            Invoke("LidMove", 3f);
        }
     
    }

    private void Update()
    {
        //答え合わせを一回だけ呼ぶ
        if (!isCalledOnce)
        {
          
           if(NextButton.CheckStart)
            {
                if (Rot == true)
                {
                    Close = true;
                    Rot = false;
                    StartCoroutine(Cl());
                }
                
                Close = true;
                Invoke("Finish", 3f);
            }
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
            Debug.Log("あってる");
        }
        else
        {
            Debug.Log("あってない");
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
            FlontClock.transform.position = Vector2.MoveTowards(
           FlontClock.transform.position,
           new Vector2(Openpositiontarget.position.x, Openpositiontarget.position.y),
           Openspeed * Time.deltaTime);
            FlontClock.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), 0.3f);
        }
        else
        {
            FlontClock.transform.position = Vector2.MoveTowards(
          FlontClock.transform.position,
          new Vector2(Closepositiontarget.position.x, Closepositiontarget.position.y),
          Openspeed * Time.deltaTime);
            FlontClock.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -180, 0), 0.3f);
        }
    }

    private void LidMove()
    {
        Close = false;
        Rot = false;
        StartCoroutine(rt());
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
}
