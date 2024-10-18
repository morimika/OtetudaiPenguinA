using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //シーン遷移先
    [SerializeField] private bool _toPlay;
    [SerializeField] private FadeAnimation _fadeAnimation;

   

    // Update is called once per frame
    void Update()
    {
        if (_fadeAnimation._fadeInEnd)
        {
            Invoke("CallChangeScene",15.0f);
            if (_toPlay) CallChangeScene();
        }
        
    }

    private void CallChangeScene()
    {
        SceneManager.LoadScene("Yuria_PlayScene");
    }
}
