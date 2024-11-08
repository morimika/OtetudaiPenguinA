using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class move : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            var Pos = this.transform.position;
            Pos.x -= Time.deltaTime * 2;
            this.transform.position = Pos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var Pos = this.transform.position;
            Pos.x += Time.deltaTime * 2;
            this.transform.position = Pos;
        }
    }
}
