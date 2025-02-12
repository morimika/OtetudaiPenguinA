using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongClock : MonoBehaviour
{
    public int LoMeter = -15;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0f, 0f, LoMeter);  // Z²‚ğ10‹‰ñ“]


        }
    }
    private void Lotate()
    {
        //transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime);
    }
}
