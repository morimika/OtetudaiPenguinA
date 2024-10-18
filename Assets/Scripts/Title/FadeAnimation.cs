using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private ChangeScene _changeScene;
    [SerializeField] private Canvas _changeAnimation;
    private Scene _nextScene;
    public bool _fadeInEnd;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _changeAnimation.gameObject.SetActive(true);
            
            _fadeInEnd = true;
            
        }
    }
}
