using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField, Scene] private int _sceneIndex;

    /// <summary>
    /// 外部からの呼び出し
    /// </summary>
    public void LoadScene()
    {
        ChangeScene.Instance.LoadNextScene(_sceneIndex);
    }
}
