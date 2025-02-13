using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_Change : MonoBehaviour
{
    private int _count = 0;
    private int _myNamber;
    private SpriteRenderer _spriteRenderer;

    Grass _grass;
    // Start is called before the first frame update
    void Start()
    {
        _myNamber = int.Parse(this.gameObject.name);
        Debug.Log(_myNamber);
        GameObject obj = GameObject.Find("Player");
        _grass = obj.GetComponent<Grass>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _count = _grass._targetNumbers.Count;
        colorChange();
    }

    private void colorChange()
    {
        if(_myNamber == _count)
        {
            _spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }
}
