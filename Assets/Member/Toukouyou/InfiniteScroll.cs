using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect _scrollRect;
    public RectTransform _viewPort_rtf;
    public RectTransform _content_rtf;
    public GridLayoutGroup _gridLayoutGroup;
    public RectTransform[] _timeList;

    private bool _isUpdated;
    private Vector2 _oldVelocity;
    void Start()
    {
        _oldVelocity = Vector2.zero;
        _isUpdated = false;

        int _addNum = Mathf.CeilToInt(_viewPort_rtf.rect.height / (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y));
        for(int i = 0;i < _addNum;i++)
        {
            RectTransform rtf =Instantiate(_timeList[i % _timeList.Length], _content_rtf);
            rtf.SetAsLastSibling();
        }
        for (int i = 0; i < _addNum; i++)
        {
            int j = _timeList.Length - 1 - i;
            while (j < 0)
            {
                j += _timeList.Length;
            }
            RectTransform rtf = Instantiate(_timeList[j], _content_rtf);
            rtf.SetAsFirstSibling();
        }

        _content_rtf.localPosition = new Vector2(_content_rtf.localPosition.x, (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * _addNum);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isUpdated)
        {
            _isUpdated = false;
            _scrollRect.velocity = _oldVelocity;
        }

        if(_content_rtf.localPosition.y < 0)
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = _scrollRect.velocity;
            _content_rtf.localPosition += new Vector3(0,_timeList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y),0);
            _isUpdated = true;
        }

        if(_content_rtf.localPosition.y > (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * _timeList.Length)
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = _scrollRect.velocity;
            _content_rtf.localPosition -= new Vector3(0, _timeList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y), 0);
            _isUpdated = true;
        }
        Debug.Log(_timeList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y));
    }
}
