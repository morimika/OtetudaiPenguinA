using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

//アイテムの種類
public enum ItemType
{
    Item,
    Seal
}


/// <summary>
/// スクリプタブルオブジェクトとしてアイテムを作成できる
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "CreateItem")]
public class ItemData : ScriptableObject
{
    [Header("アイテム情報")]
    [Label("アイコン画像")]
    public Sprite icon;        // アイテムのアイコン画像
    [Label("種類")]
    public ItemType itemType;  // アイテムの種類
    [Label("名前")]
    public string itemName;    // アイテムの名前
    [Label("アイテムID")]
    public int itemId;    // アイテムのID
    [TextArea,Label("アイテム説明")]
    public string description; // アイテムの説明
}
