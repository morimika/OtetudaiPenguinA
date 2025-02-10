using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MoveGaer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private int _index;

    #region ÉhÉâÉbÉOèàóù

    private bool _isDragging = false;
    private Vector3 _diffPostision;
    private bool _isAttach = false;
    private System.Action<int> _callback;
    private bool _isEnable = true;

    public void Setup(System.Action<int> callback)
    {
        _callback = callback;
    }

    public void SetGearEnable(bool enable)
    {
        _isEnable = enable;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isEnable == false) return;
        _isDragging = true;
        var startPos = Camera.main.ScreenToWorldPoint(eventData.position);
        _diffPostision = startPos - transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isEnable == false) return;
        if (_isDragging)
        {
            var tapPos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = tapPos - _diffPostision;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    #endregion

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TargetCenter") && _isDragging == false && _isAttach == false)
        {
            _isAttach = true;
            transform.DOMove(other.transform.position, 0.2f).OnComplete(() => _callback?.Invoke(_index));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isAttach = false;
    }
}