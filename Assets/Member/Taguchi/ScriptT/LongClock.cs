using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongClock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Lotate()
    {
        transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime);
    }
}
