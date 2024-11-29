using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvas : MonoBehaviour
{
    //”wŒi‚ÌƒCƒ[ƒW      
    public GameObject B1;
    public GameObject B2;
    public GameObject B3;
    public GameObject B4;

    //ˆÚ“®‘¬“x
    public int MoveSpeed = 5;

    //”÷’²ß
    public float R;
    public float Timer;

    private bool BA1 = false;
    private bool BA2 = false;
    private bool BA3 = false;
    private bool BA4 = false;

    //Œo‰ßŠÔ
    float _time;
    async void Start()
    {
        DontDestroyOnLoad(gameObject);
        //GameObject‚ª”jŠü‚³‚ê‚½‚ÉƒLƒƒƒ“ƒZƒ‹‚ğ”ò‚Î‚·ƒg[ƒNƒ“‚ğì¬
        var token = this.GetCancellationTokenOnDestroy();
        
        //
        //await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        BA1 = true;
        //
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        BA2 = true;
        //
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        BA3 = true;
        //
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        BA4 = true;
      
    }
    //x1 y2

    // Update is called once per frame
    void Update()
    {
        if(BA1 == true)
        {
            B1.transform.position += new Vector3(0.5f * (MoveSpeed+R), -1*(MoveSpeed), 0);//MoveSpeed1 * Time.deltaTime
                                                              //‚PƒtƒŒ[ƒ€‚ ‚½‚è‚Ì•b”‚ğ‰ÁZ‚·‚é
            _time += Time.deltaTime;
        }
        if (BA2 == true)
        {
            B2.transform.position += new Vector3(0.5f * (MoveSpeed + R), -1 * (MoveSpeed), 0);//MoveSpeed1 * Time.deltaTime
                                                                                          //‚PƒtƒŒ[ƒ€‚ ‚½‚è‚Ì•b”‚ğ‰ÁZ‚·‚é
            _time += Time.deltaTime;
        }
        if (BA3 == true)
        {
            B3.transform.position += new Vector3(0.5f * (MoveSpeed + R), -1 * (MoveSpeed), 0);//MoveSpeed1 * Time.deltaTime
                                                                                          //‚PƒtƒŒ[ƒ€‚ ‚½‚è‚Ì•b”‚ğ‰ÁZ‚·‚é
            _time += Time.deltaTime;
        }
        if (BA4 == true)
        {
            B4.transform.position += new Vector3(0.5f * (MoveSpeed + R), -1 * (MoveSpeed), 0);//MoveSpeed1 * Time.deltaTime
                                                                                          //‚PƒtƒŒ[ƒ€‚ ‚½‚è‚Ì•b”‚ğ‰ÁZ‚·‚é
            _time += Time.deltaTime;
        }
        Invoke(nameof(Destro),5f);
    }
    private void Destro()
    {
        Destroy(this);
    }
}
