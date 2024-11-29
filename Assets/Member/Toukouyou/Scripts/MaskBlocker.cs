using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskBlocker : MonoBehaviour, IPointerClickHandler
{
    // 点击遮罩层时的回调（什么都不做，只是拦截事件）
    public void OnPointerClick(PointerEventData eventData)
    {
        // Do nothing
    }
}