using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavingItem : MonoBehaviour
{
    [SerializeField]
    private ItemList _playerItem;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("アイテム量：" + _playerItem.items.Count);
    }
}
