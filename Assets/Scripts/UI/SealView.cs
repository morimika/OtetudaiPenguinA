using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Mori Script

/// <summary>
/// シールやアイテムを並べて表示するスクリプト
/// </summary>
public class SealView : MonoBehaviour
{
    [SerializeField, Label("シールリスト")]
    private ItemList _sealList;

    [SerializeField, Header("空のイメージ")]
    private Image _enptyImage;

    [SerializeField, Header("空のオブジェクト")]
    private GameObject _enptyObj;
    [SerializeField, Label("オブジェクト用：並べるシールの間隔")]
    private float _sealInterval = 2;

    void Start()
    {
        if (_enptyImage != null)
        {
            //panelにアタッチしLayoutグループを参考に並べていく
            for (int i = 0; i < _sealList.items.Count; i++)
            {
                //Panelを親として生成
                var ima = Instantiate(_enptyImage, transform);
                //アイテム情報を取得してアイコンを表示
                var spr = _sealList.items[i];
                ima.sprite = spr.icon;
            }
        }
        if (_enptyObj != null)
        {
            //ゲームオブジェクトにアタッチしてその子にする
            for (int i = 0; i < _sealList.items.Count; i++)
            {
                //Layoutグループが使えないのでここで並べる、子として生成
                var ima = Instantiate(_enptyObj
                    ,new Vector2(this.transform.position.x+(i*_sealInterval)
                                ,this.transform.position.y)
                    ,Quaternion.identity
                    ,transform);
                //アイテム情報を取得してアイコンを表示
                var spr = _sealList.items[i];
                ima.GetComponent<SpriteRenderer>().sprite = spr.icon;
            }
        }
    }
}
