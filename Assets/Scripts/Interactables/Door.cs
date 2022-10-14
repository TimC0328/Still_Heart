using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private string nextRoom;
    [SerializeField]
    private bool isLocked = false;
    [SerializeField]
    private string targetKey = "";

    [SerializeField]
    private Vector3 exitPos, exitRot;

    [SerializeField]
    private string targetCam = "";

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        GameManager.Instance.player.SetState(3);
        if (!isLocked)
            ChangeRoom();
        else if (Unlock())
            return;
        else
            interactUI.DisplayInteractText(this, text);
    }

    private void ChangeRoom()
    {
        GameManager.Instance.ChangeScene(nextRoom, exitPos, exitRot, targetCam);
    }

    public bool Unlock()
    {
        KeyItem key = Inventory.Instance.SearchKeyItem(targetKey);
        if (key != null)
        {
            isLocked = false;
            interactUI.DisplayInteractText(this, key.GetUseText());
            GameManager.Instance.roomManager.ModifyDoorStatus(gameObject.name, isLocked);
            return true;
        }
        else
            return false;

    }

    public override void OnMessageEnd()
    {
        InteractionEnd(0);
    }

    public void SetLocked(bool state)
    {
        isLocked = state;
    }
}
