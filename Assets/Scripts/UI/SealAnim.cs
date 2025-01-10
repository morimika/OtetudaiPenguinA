using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;


//Mori Script

[RequireComponent(typeof(Image))]
public class SealAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.gameObject.transform.localScale = Vector3.one * 1.5f;
        }
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerDown(PointerEventData eventData)
    {
        if (image.sprite.name != "Background")
        {
            this.gameObject.transform.DOScale(2f, 0.2f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = Vector3.one * 1.5f;
    }
}
