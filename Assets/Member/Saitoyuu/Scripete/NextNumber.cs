using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextNumber : MonoBehaviour
{
    [SerializeField] GameObject _nextNumberGameObject;
    [SerializeField] GameObject _endNumberGameObject;
    public int _count = 0;
    Grass _grass;

    private void Start()
    {
        GameObject obj = GameObject.Find("Player");
        _grass = obj.GetComponent<Grass>();
    }

    private void Update()
    {
        _count = _grass._targetNumbers.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("óvëfêî");
        Debug.Log(_count);
        if ((_count == 10) || (_count == 29) || (_count == 59) || (_count == 108))
            Invoke(("NextActive"), 2.0f);

    }

    private void NextActive()
    {
        _endNumberGameObject.SetActive(false);
        _nextNumberGameObject.SetActive(true);
        Debug.Log("éüÇÃêîéö");
    }

}
