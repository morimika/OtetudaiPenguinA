using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Follow : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 pos;
    [SerializeField,Range(0f,1f)] float _followStrength ;
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
            pos.z = 0;
            transform.position = Vector3.Lerp(transform.position, pos, _followStrength);

        }
    }

}
