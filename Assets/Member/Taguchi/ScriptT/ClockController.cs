using System;
using DG.Tweening;	//DOTween‚ğg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ğ“ü‚ê‚é

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public bool sec;
    public bool secTick;
    public GameObject hour;
    public GameObject minute;
    public GameObject second;

    void Start()
    {
        if (!sec)
            Destroy(second); // •bj‚ğÁ‚·
    }

    void Update()
    {
        DateTime dt = DateTime.Now;

        hour.transform.eulerAngles = new Vector3(0, 0, (float)dt.Hour / 12 * -360 + (float)dt.Minute / 60 * -30);
        minute.transform.eulerAngles = new Vector3(0, 0, (float)dt.Minute / 60 * -360);
        if (sec)
        {
            if (secTick)
                second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360);
            else
                second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360 + (float)dt.Millisecond / 60 / 1000 * -360);
        }

    }
}
