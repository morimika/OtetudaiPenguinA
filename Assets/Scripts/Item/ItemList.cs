using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemList : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
}
