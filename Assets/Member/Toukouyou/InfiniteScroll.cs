using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect _scrollRect;
    public RectTransform _viewPort_rtf;
    public RectTransform _content_rtf;
    public GridLayoutGroup _gridLayoutGroup;
    public RectTransform[] _timeList;
    private int _addNum;
    private bool _isUpdated;
    private Vector2 _oldVelocity;

    private bool _isSnapped;
    float _snapSpeed;
    public float _snapForce;
    void Start()
    {
        _oldVelocity = Vector2.zero;
        _isUpdated = false;
        _isSnapped = false;
        _addNum = Mathf.CeilToInt(_viewPort_rtf.rect.height / (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y));
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

    void Update()
    {
        if (_isUpdated)
        {
            _isUpdated = false;
            _scrollRect.velocity = _oldVelocity;
        }

        if (_content_rtf.localPosition.y < 0)
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = _scrollRect.velocity;
            _content_rtf.localPosition += new Vector3(0, _timeList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y), 0);
            _isUpdated = true;
        }

        if (_content_rtf.localPosition.y > (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * _timeList.Length)
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = _scrollRect.velocity;
            _content_rtf.localPosition -= new Vector3(0, _timeList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y), 0);
            _isUpdated = true;
        }
        
        int currentTime =  (Mathf.RoundToInt(_content_rtf.localPosition.y / (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y))-_addNum + _timeList.Length)% _timeList.Length;

        var a = _timeList[currentTime].GetComponent<TextMeshProUGUI>();
        var b = _timeList[(currentTime + 1) % _timeList.Length].GetComponent<TextMeshProUGUI>();
        var c = _timeList[(currentTime + 2) % _timeList.Length].GetComponent<TextMeshProUGUI>();
        Debug.Log(currentTime);
        //Debug.Log("a:" + _timeList[currentTime]);
        //Debug.Log("b:" + _timeList[(currentTime + 1) % _timeList.Length]);
        //Debug.Log("c:" + _timeList[(currentTime + 2) % _timeList.Length]);
        if (a.fontSize == 36 && c.fontSize == 36)
        {
            if (b.fontSize < 80)
            {
                b.fontSize += 11;
            }

        }
        else
        {
            a.fontSize = 36;
            c.fontSize = 36;
        }
        var ba = GameObject.Find("23(Clone)").GetComponent<TextMeshProUGUI>();
        var sd = GameObject.Find("24(Clone)").GetComponent<TextMeshProUGUI>();
        if (currentTime ==21)
        {
           
            if (ba.fontSize < 80)
            {
                ba.fontSize += 11;
            }
        }
        else
        {
            ba.fontSize = 36;
        }
        if(currentTime ==22)
        {
            if (sd.fontSize < 80)
            {
                sd.fontSize += 11;
            }
        }
        else
        {
            sd.fontSize = 36;
        }

        if (_scrollRect.velocity.magnitude < 50 && !_isSnapped)
        {
            _scrollRect.velocity = Vector2.zero;
            _snapSpeed += Time.deltaTime * _snapForce;
            _content_rtf.localPosition = new Vector3(_content_rtf.localPosition.x,
                                                    Mathf.MoveTowards(_content_rtf.localPosition.y, ((currentTime * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) + _content_rtf.rect.height) % _content_rtf.rect.height + (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * _addNum) % (_content_rtf.rect.height + _gridLayoutGroup.spacing.y -  _addNum *(_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y)), _snapSpeed),
                                                     _content_rtf.localPosition.z);
            if (_content_rtf.localPosition.y == (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) + (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * _addNum)
            {
                _isSnapped = true;
            }
        }

        if (_scrollRect.velocity.magnitude > 50)
        {
            _isSnapped = false;
            _snapSpeed = 0;
        }

    }
}
