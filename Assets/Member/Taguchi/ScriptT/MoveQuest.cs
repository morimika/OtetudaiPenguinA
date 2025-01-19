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

    public float maxDistanceDelta = 2.0f;

    private bool Swich = false;

    public float MoveSpeed = 10;
    void Start()
    {
        Invoke("Wait",WaitTime);
        _textCountdown.text = "";
        Swich = false;
    }

    void Update()
    {
        Move();
    }

    private void Wait()
    {
        Swich = true;
 
       

        // this.transform.position = new Vector3(-490, 380, 0);
        Invoke("StartCount", 1.5f);
    }

    private void Move()
    {
        if(Swich == true)
        {
            Vector3 current = transform.position;
            Vector3 target = new Vector3(750, 720, 0);//問題文の移動先の座標指定
            float step = 2.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(current, target, maxDistanceDelta);
        }
    }
	 
    public void StartCount()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "すたーと！";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);
    }
   
}
