using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
    }
}
