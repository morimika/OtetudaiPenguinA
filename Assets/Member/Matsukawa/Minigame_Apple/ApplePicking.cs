using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーがappleに触れたのを感知するクラス
public class ApplePicking : MonoBehaviour
{

    public void OnClickAct()
    {
        // 木についてる(画面に表示されている)appleは消える
        Destroy(this.gameObject);

        // 1回クリックするとかごにあるappleの数が増える
        AppleController.Count++;
    }
}
