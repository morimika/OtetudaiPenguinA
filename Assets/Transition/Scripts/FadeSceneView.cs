using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneView : SingletonMonoBehaviour<FadeSceneView>
{
    
    [System.Serializable]
    public class FadePositions
    {
        public Vector3 startPos;
        public Vector3 centerPos;
        public Vector3 endPos;
    }

    [SerializeField] private List<Transform> _fadePanels = new List<Transform>();
    private List<FadePositions> _fadePositions = new List<FadePositions>()
    {
        new FadePositions{startPos = new Vector3(-1233, 1233,0),centerPos = new Vector3(527,-527,0), endPos = new Vector3(2562, -2562,0)},
        new FadePositions{startPos = new Vector3(-1576, 1576,0),centerPos = new Vector3(184,-184,0), endPos = new Vector3(2219, -2219,0)},
        new FadePositions{startPos = new Vector3(-2123, 2123,0),centerPos = new Vector3(-383,383,0), endPos = new Vector3(1672, -1672,0)},
        new FadePositions{startPos = new Vector3(-2522, 2522,0),centerPos = new Vector3(-762,762,0), endPos = new Vector3(1273, -1273,0)},
    };

    [SerializeField] private float _delayTime = 0.05f;

    public void LoadNextScene(int sceneIndex)
    {
        
        
        LoadNextSceneASync(sceneIndex).Forget();
    }

    private async UniTask LoadNextSceneASync(int sceneIndex)
    {
        await FadeIn();
        await SceneManager.LoadSceneAsync(sceneIndex);
        await FadeOut();
    }

    /// <summary>
    /// パネルが画面に入ってくる
    /// </summary>
    /// <returns></returns>
    private async UniTask FadeIn()
    {
        bool _isExit = false;
        _ = _fadePanels[0].DOLocalMove(_fadePositions[0].centerPos, 1);
        _ = _fadePanels[1].DOLocalMove(_fadePositions[1].centerPos, 1).SetDelay(_delayTime);
        _ = _fadePanels[2].DOLocalMove(_fadePositions[2].centerPos, 1).SetDelay(_delayTime * 2);
        _ = _fadePanels[3].DOLocalMove(_fadePositions[3].centerPos, 1).SetDelay(_delayTime * 3).OnComplete(() => _isExit = true);
        await UniTask.WaitUntil(() => _isExit);
    }

    /// <summary>
    /// パネルが画面買いに出る
    /// </summary>
    /// <returns></returns>
    private async UniTask FadeOut()
    {
        bool _isExit = false;
        _ = _fadePanels[0].DOLocalMove(_fadePositions[0].endPos, 1);
        _ = _fadePanels[1].DOLocalMove(_fadePositions[1].endPos, 1).SetDelay(_delayTime);
        _ = _fadePanels[2].DOLocalMove(_fadePositions[2].endPos, 1).SetDelay(_delayTime * 2);
        _ = _fadePanels[3].DOLocalMove(_fadePositions[3].endPos, 1).SetDelay(_delayTime * 3).OnComplete(() => _isExit = true);
        await UniTask.WaitUntil(() => _isExit);
        _fadePanels[0].localPosition = _fadePositions[0].startPos;
        _fadePanels[1].localPosition = _fadePositions[1].startPos;
        _fadePanels[2].localPosition = _fadePositions[2].startPos;
        _fadePanels[3].localPosition = _fadePositions[3].startPos;
    }
    
    
}
