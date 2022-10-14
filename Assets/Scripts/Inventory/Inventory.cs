using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance { get { return _instance; } }

    public List<Item> inventory;

    public List<Weapon> weapons;

    [SerializeField]
    private int maxItems = 6;

    [SerializeField]
    private GameObject inventoryUI;


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
        if (inventory.Count == 0)
            return null;

        foreach(Item item in inventory)
        {
            if (!(item is KeyItem))
                continue;
            if (item.GetItemName() == target)
                return (KeyItem)item;
        }
        return null;
    }

    public Item SearchItem(string target)
    {
        if (inventory.Count == 0)
            return null;

        foreach (Item item in inventory)
        {

            if (item.GetItemName() == target)
                return item;
        }
        return null;
    }

    public bool AddItem(Item item)
    {
        if (inventory.Count == maxItems)
            return false;

        inventory.Add(item);
        return true;
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }

    public bool ToggleUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        return inventoryUI.activeSelf;
    }
}
