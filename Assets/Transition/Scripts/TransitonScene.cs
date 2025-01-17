using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TransitonScene : MonoBehaviour
{
    [SerializeField, Scene] private int _sceneIndex;

    /// <summary>
    /// 外部からの呼び出し
    /// </summary>
    public void LoadScene()
    {
        BSJSoundManger.Instance.PlayBGM(1);
        FadeSceneView.Instance.LoadNextScene(_sceneIndex);
    }
}
