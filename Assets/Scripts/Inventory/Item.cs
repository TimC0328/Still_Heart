using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    protected string itemName;

    public string GetItemName()
    {
        return itemName;
    }
}
