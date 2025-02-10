using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor_View : MonoBehaviour
{
    [SerializeField] GameObject _nextNumberGameObject;
    [SerializeField] GameObject _endNumberGameObject;
    public int _count = 0;
    Grass _grass;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        _grass = obj.GetComponent<Grass>();
    }

    // Update is called once per frame
    void Update()
    {
        _count = _grass._targetNumbers.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("óvëfêî");
        Debug.Log(_count);
        if ((_count == 18) || (_count == 39) || (_count == 47) || (_count == 65) || (_count == 77) ||(_count == 79)|| (_count == 82)|| (_count == 97)|| (_count == 90))
            Invoke(("NextAnchor"), 0.5f);

    }

    private void NextAnchor()
    {
        _endNumberGameObject.SetActive(false);
        _nextNumberGameObject.SetActive(true);
        Debug.Log("ñÓàÛ");
    }
}