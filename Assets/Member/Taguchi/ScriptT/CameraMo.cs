using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMo : MonoBehaviour
{
  //  [SerializeField] public GameObject ActionCamera;

    Vector3 CameraTransform;//時計を真ん中にする座標を保存しておく場所
    public float MoveSpeed = 1.0f;

    public NextButton NextButton;//スクリプトの変数を引っ張ってくるよう

  
    void Start()
    {
        CameraTransform = new Vector3(4, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
       //ボタンを押されたら時計が真ん中に移る座標に移動させる
        if(NextButton != null && NextButton.GoCamera)
        {
            this.gameObject.transform.position =
       Vector3.MoveTowards(transform.position, CameraTransform, MoveSpeed);
        }
        
    }
    
}
