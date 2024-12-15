using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraFollow : MonoBehaviour
{
    public UnityEngine.Transform Player;
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        transform.position = targetPosition;
    }
}