using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClock : MonoBehaviour
{
    private bool ClockOpen = false;
    private bool ClockClose = true;
    public void Open()
    {
        if (ClockOpen == false)
        {
            this.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
            ClockOpen = true;
            ClockClose = false;
        }
        
    }
    public void Close()
    {
        if (ClockClose == false)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            ClockOpen = false;
            ClockClose = true;
        }

    }
}
