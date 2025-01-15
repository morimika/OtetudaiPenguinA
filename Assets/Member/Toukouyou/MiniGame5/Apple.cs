using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private Vector2 _mousePos;
    private Vector2 _distance;
    private Vector2 _startPos;
    private Transform _startParent;
    private Rigidbody2D _rigidbody;
    private bool _isCorrect;
    [SerializeField] private List<Transform> _plates = new List<Transform>();
    private bool _isEmpty = true;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _startPos = transform.position;
        _startParent = transform.parent;
 
        _mousePos = Vector2.zero; 
        _distance = Vector2.zero;
    }

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        _distance = new Vector2(transform.position.x,transform.position.y) - _mousePos;
    }
    private void OnMouseDrag()
    {
        transform.position = _mousePos + _distance;
    }
    private void OnMouseUp()
    {
        if (_isCorrect && _isEmpty)
        {
            if (_mousePos.x <= (_plates[0].transform.position.x + _plates[1].transform.position.x) / 2)
            {
                this.transform.position = _plates[0].transform.position;
                this.transform.SetParent(_plates[0].transform, true);
            }
            else if (_mousePos.x < (_plates[1].transform.position.x + _plates[2].transform.position.x) / 2 && _mousePos.x > (_plates[0].transform.position.x + _plates[1].transform.position.x) / 2)
            {
                this.transform.position = _plates[1].transform.position;
                this.transform.SetParent(_plates[1].transform, true);
            }
            else if (_mousePos.x < (_plates[2].transform.position.x + _plates[3].transform.position.x) / 2 && _mousePos.x > (_plates[1].transform.position.x + _plates[2].transform.position.x) / 2)
            {
                this.transform.position = _plates[2].transform.position;
                this.transform.SetParent(_plates[2].transform, true);
            }
            else if (_mousePos.x < (_plates[3].transform.position.x + _plates[4].transform.position.x) / 2 && _mousePos.x > (_plates[2].transform.position.x + _plates[3].transform.position.x) / 2)
            {
                this.transform.position = _plates[3].transform.position;
                this.transform.SetParent(_plates[3].transform, true);
            }
            else if (_mousePos.x >= (_plates[3].transform.position.x + _plates[4].transform.position.x) / 2)
            {
                this.transform.position = _plates[4].transform.position;
                this.transform.SetParent(_plates[4].transform, true);
            }
        }
        else
        {
            this.transform.position = _startPos;
            this.transform.SetParent(_startParent, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Friend"))
        {
            _isCorrect = true;

            if (collision.transform.childCount == 0)
            {
                _isEmpty = true;
            }
            else
            {
                _isEmpty = false;
            }
        }
        else
        {
            _isCorrect = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         _isCorrect = false;
         _isEmpty = true;
    }
}
