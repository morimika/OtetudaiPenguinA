using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class MoveQuest : MonoBehaviour
{
    [SerializeField] public float WaitTime = 3.0f;

    [SerializeField]
    private Text _textCountdown;


    private Vector3 scale;

    [SerializeField]
    private GameObject _imageMask;

    public float maxDistanceDelta = 1.0f;

    private bool Swich = false;

    public int XPosition = 700;
    public int YPosition = 780;

    //public GameObject QuestPanel;
  

    public bool ThisQuestTitle = true;

    public float MoveSpeed = 10;

    //4びょう
    public GameObject BG;
    public GameObject Mondai;
    void Start()
    {
        Invoke("Wait", WaitTime);
        _textCountdown.text = "";
        Swich = false;

        Invoke("MoveMondaiBun", 4f);
    }

    void Update()
    {
        Move();
       
    }

    private void Wait()
    {
        Swich = true;
        StartCoroutine("ScaleDown");//問題の大きさをだんだん小さくするコルーチンを呼ぶ
        Invoke("UnLock", 1f);


    }

    private void Move()
    {
        if (Swich == true)
        {
            Vector3 current = transform.position;
            Vector3 target = new Vector3(600, 930, 0);//問題文の移動先の座標指定
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(current, target, maxDistanceDelta);
        }

    }

    IEnumerator ScaleDown()
    {
        for (float i = 2.5f; i > 1.3; i -= 0.01f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return null;new WaitForSeconds(0.1f);
        }
    }

    //最初のギアを触れないようにする制限を解除する
    private void UnLock()
    {
       // QuestPanel.SetActive(true);
        _imageMask.gameObject.SetActive(false);
    }
    private void MoveMondaiBun()
    {
        Mondai.SetActive(true);
        BG.SetActive(true);
        
    }
}