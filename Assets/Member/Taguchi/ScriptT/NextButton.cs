using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float DeleteTime = 0.5f;
    void Start()
    {
        CameraTransform = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {    
    }
    public void Anser()
    {
        /* ActionCamera.transform.position =
         Vector3.MoveTowards(transform.position, CameraTransform, MoveSpeed);*/
        GoCamera = true;//別スクリプトで使う
        CheckStart = true;
        Destroy(MondaiPanel);//問題文を消す
       // Destroy(this.gameObject,DeleteTime);//指定した時間後にこのオブジェクトを削除する
    }
}
