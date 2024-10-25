using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

/// <summary>
/// プレイヤーの持つアイテムリストとして
/// スクリプタブルオブジェクトをリストに格納
/// </summary>
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemList : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
}
