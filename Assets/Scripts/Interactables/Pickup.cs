using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField]
    private Item item;
    private bool pickedUp;


    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        if (Inventory.Instance.AddItem(item))
        {
            text.Add("*Picked up " + item.GetItemName() + "*");
            pickedUp = true;
        }
        else
        {
            text.Add("Not enough inventory space");
            pickedUp = false;
        }
        base.Interact();
    }

    protected override void InteractionEnd(int newState)
    {
        GameManager.Instance.player.SetState(newState);
        if(pickedUp)
            Destroy(gameObject);
    }
}
