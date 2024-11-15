using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Follow : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 pos;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            Debug.Log(hit.collider.gameObject.transform.position);
        }
        mousePos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
            pos.z = 0;
            transform.position = pos;

        }
    }
  
}
