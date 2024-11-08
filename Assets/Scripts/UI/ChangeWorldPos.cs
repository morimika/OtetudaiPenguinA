using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChangeWorldPos : MonoBehaviour
{
    // オブジェクトを映すカメラ
    Camera targetCamera;

    private void Start()
    {
        targetCamera=Camera.main;
    }

    private void Update()
    {
        var targetScreenPos = this.transform.position;

        var targetWorldPos = targetCamera.ScreenToWorldPoint(targetScreenPos);

        this.transform.position = targetWorldPos;

    }
}
