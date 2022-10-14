using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    protected string itemName;

    [SerializeField]
    protected List<string> itemDescription;

    [SerializeField]
    protected bool equip;

    [SerializeField]
    protected Item combineTarget;
    [SerializeField]
    protected Item combineResult;

    [SerializeField]
    protected GameObject displayModel;
    [SerializeField]
    protected Vector3 modelPos;

    public int quantity;

    public string GetItemName()
    {
        return itemName;
    }

    public GameObject DisplayModel(Transform ui)
    {
        GameObject temp;
        temp = Instantiate(displayModel, ui);
        temp.transform.localPosition = modelPos;
        temp.transform.GetChild(0).gameObject.AddComponent<RotateItem>();
        return temp;
    }

    public bool Equip()
    {
        if (!equip)
            return false;

        int weaponIndex = 0;
        
        switch(itemName)
        {
            case "Pistol":
                weaponIndex = 1;
                break;
            case "":
                break;
        }

        Inventory.Instance.weapons[weaponIndex].Equip();
        GameManager.Instance.player.GetComponent<CombatSystem>().ChangeWeapon(Inventory.Instance.weapons[weaponIndex]);

        return true;
    }

    public List<string> Inspect()
    {
        return itemDescription;
    }

    public bool Combine(Item target)
    {
        if (target != combineTarget || target.GetItemName() != combineTarget.GetItemName())
            return false;

        Inventory.Instance.RemoveItem(this);
        Inventory.Instance.RemoveItem(target);

        Inventory.Instance.AddItem(combineResult);
        return true;
    }
}
