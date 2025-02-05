using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveMondai : MonoBehaviour
{
    public float MoveSpeed = 10;
    // public float maxDistanceDelta = 1.0f;

    public float Xpos = 500;
    public float Ypos = 500;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        Vector3 target = new Vector3(Xpos, Ypos, 0);//問題文の移動先の座標指定
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(current, target, MoveSpeed);
        StartCoroutine("ScaleDown");//問題の大きさをだんだん小さくするコルーチンを呼ぶ
    }
    IEnumerator ScaleDown()
    {
        for (float i = 0; i < 3.63f; i+= 0.1f)
        {
            transform.localScale = new Vector3((i+0.14f), i , 1);
            yield return null; new WaitForSeconds(0.1f);
        }
    }
}
/* 
3.77
3.63
    }*/