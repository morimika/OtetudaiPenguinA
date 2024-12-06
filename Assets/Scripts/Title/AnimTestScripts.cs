using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTestScripts : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FadeView _fadeView;

    public void Setup(System.Action onComplete)
    {
        _fadeView.Setup(onComplete);
    }
    
    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }
}
