using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_Change : MonoBehaviour
{
    public int _count = 0;
    private int _myNamber;
    Grass _grass;
    // Start is called before the first frame update
    void Start()
    {
        _myNamber = int.Parse(this.gameObject.name);
        Debug.Log(_myNamber);
        GameObject obj = GameObject.Find("Player");
        _grass = obj.GetComponent<Grass>();
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
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }
}
