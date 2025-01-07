using Cysharp.Threading.Tasks.Triggers;
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
    [SerializeField, Label("全シールリスト")]
    private ItemList _allSealList;

    [SerializeField, Label("所持シールリスト")]
    public ItemList _sealList;

    [SerializeField, Header("空のイメージ")]
    private Image _enptyImage;
    [SerializeField, Header("空白")]
    private Image _noneImage;

    [SerializeField, Header("空のオブジェクト")]
    private GameObject _enptyObj;
    [SerializeField, Label("オブジェクト用：並べるシールの間隔")]
    private float _sealInterval = 2;

    void Start()
    {
        SetSealCanvas();
    }

    [SerializeField,Button]
    public void ClearSeals()
    {
        _sealList.items.Clear();
    }

    [SerializeField, Button]
    public void RefreshCanvasData()
    {
        //子オブジェクトを消去
        //自分の子供を全て調べる
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var cld = gameObject.transform.GetChild(i);
            var spr = cld.GetComponent<Image>();
            spr.sprite = null;
        }
        SetSealData();
    }

    [SerializeField, Button]
    public void SetSealCanvas()
    {
        if (_enptyImage != null)
        {
            int v = 0;
            int e = 0;
            //panelにアタッチしLayoutグループを参考に並べていく
            //デフォルトのシール
            for (int i = 0; i < _allSealList.items.Count+e; i++)
            {
                //2個毎に間をあける
                if(v<2)
                {
                    //Panelを親として生成
                    var ima = Instantiate(_enptyImage, transform);
                    //アイテムIDのために名前をナンバリングする
                    ima.gameObject.name = (i - e+1).ToString();
                    v++;
                }
                else
                {
                    //空白部分
                    v = 0;
                    e++;
                    //透明なオブジェクトを生成
                    Image ins =Instantiate(_noneImage, transform);
                    ins.color = new Color(0, 0, 0, 0);
                }
            }
            SetSealData();
        }


        if (_enptyObj != null)
        {
            //ゲームオブジェクトにアタッチしてその子にする
            for (int i = 0; i < _allSealList.items.Count; i++)
            {
                //Layoutグループが使えないのでここで並べる、子として生成
                var ima = Instantiate(_enptyObj
                    , new Vector2(this.transform.position.x + (i * _sealInterval)
                                , this.transform.position.y)
                    , Quaternion.identity
                    , transform);
                //アイテム情報を取得してアイコンを表示
                var spr = _allSealList.items[i];
                ima.GetComponent<SpriteRenderer>().sprite = spr.icon;
            }
        }
    }

    [SerializeField,Button]
    public void SetSealData()
    {
        //持っているシールを反映する
        for (int i = 0; i < _sealList.items.Count; i++)
        {
            //アイテムIDと同じ場所を見つける
            var tra = gameObject.transform.Find(_sealList.items[i].itemId.ToString());
            //アイテム情報、Imageコンポーネントを取得してアイコンを反映
            var spr = _sealList.items[i].icon;
            var obj = tra.GetComponent<Image>();
            obj.sprite = spr;
        }
    }
}
