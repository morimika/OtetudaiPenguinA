using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Mori Script

public class TouchObjAnim : MonoBehaviour
{
    private Animator _uiTouchAnim;

    private void Start()
    {
        _uiTouchAnim=GetComponent<Animator>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            _uiTouchAnim.SetTrigger("_isTouchDown");
        }
        else if(EventSystem.current.IsPointerOverGameObject())
        {
            //_uiTouchAnim.SetBool("_isTouch", false);
            //_uiTouchAnim.SetTrigger("_doTouchUp");
        }
    }

    public void OnMouseDrag()
    {
        Debug.Log("HI");
        _uiTouchAnim.SetBool("_isTouch", true);
    }

    public void OnMouseUp()
    {
        Debug.Log("HIIP");
        _uiTouchAnim.SetBool("_isTouch", false);
        _uiTouchAnim.SetTrigger("_doTouchUp");
    }
}
