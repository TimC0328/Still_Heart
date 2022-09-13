using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField]
    private Item item;
    private bool pickedUp = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
    }

    protected override void InteractableMessage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lineNum++;
            if (lineNum < interactMessage.Length)
                interactUI.DisplayInteractText(interactMessage[lineNum]);
            else if (!pickedUp)
            {
                if (Inventory.Instance.AddItem(item))
                {
                    interactUI.DisplayInteractText("*" + item.GetItemName() + " was added to inventory.*");
                    pickedUp = true;
                }
                else
                {
                    interactUI.DisplayInteractText("*You don't have enough inventory space*");
                    lineNum = 1;
                }
            }
            else
                OnMessageEnd();
        }
    }

    protected override void InteractionEnd(int newState)
    {
        GameManager.Instance.player.SetState(newState);
        if(pickedUp)
            Destroy(gameObject);
    }
}
