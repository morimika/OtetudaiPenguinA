using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SealView : MonoBehaviour
{
    [SerializeField, Label("シールリスト")]
    private ItemList _sealList;

    [SerializeField, Header("空のオブジェクト")]
    private Image _enptyImage;

    void Start()
    {
        for(int i=0;i<_sealList.items.Count;i++) 
        {
            var ima = Instantiate(_enptyImage, transform);
            var spr = _sealList.items[i];
            ima.sprite = spr.icon;
        }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
