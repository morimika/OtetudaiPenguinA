using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            var Pos = this.transform.position;
            Pos.x -= Time.deltaTime * speed;
            this.transform.position = Pos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var Pos = this.transform.position;
            Pos.x += Time.deltaTime * speed;
            this.transform.position = Pos;
        }
    }
}
