using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Item,
    Seal
}


[CreateAssetMenu(fileName = "NewItem", menuName = "CreateItem")]
public class ItemData : ScriptableObject
{
    [Header("Item Properties")]
    public Sprite icon;        // アイテムのアイコン画像
    public ItemType itemType;  // アイテムの種類
    public string itemName;    // アイテムの名前
    [TextArea]
    public string description; // アイテムの説明
}
