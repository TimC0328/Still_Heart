using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private string nextRoom;
    [SerializeField]
    private bool isLocked = false;
    [SerializeField]
    private string targetKey = "";

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        if (!isLocked)
            ChangeRoom();
        else if (Unlock())
            return;
        else
            base.Interact();
    }

    private void ChangeRoom()
    {
        GameManager.Instance.ChangeScene(nextRoom);
    }

    public bool Unlock()
    {
        KeyItem key = Inventory.Instance.SearchKeyItem(targetKey);
        if (key != null)
        {
            isLocked = false;
            interactUI.DisplayInteractText("Used the " + key.GetItemName() + " to unlock the " + interactName + ".");
            return true;
        }
        else
            return false;

    }

    public void SetLocked(bool state)
    {
        isLocked = state;
    }
}
