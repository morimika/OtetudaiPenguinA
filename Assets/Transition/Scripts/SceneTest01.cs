using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTest01 : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private TransitonScene _transitionScene;

    // Start is called before the first frame update
    void Start()
    {
        _nextButton.onClick.AddListener(() => _transitionScene?.LoadScene());
    }

    private void OnDestroy() 
    {
        _nextButton.onClick.RemoveAllListeners();
    }
}
