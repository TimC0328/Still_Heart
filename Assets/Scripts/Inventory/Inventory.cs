using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance { get { return _instance; } }

    public List<Item> inventory;
    public List<KeyItem> keyItems;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public KeyItem SearchKeyItem(string target)
    {
        if (keyItems == null || keyItems.Count == 0)
            return null;

        foreach(KeyItem keyItem in keyItems)
        {
            if (keyItem.GetItemName() == target)
                return keyItem;
        }
        return null;
    }
}
