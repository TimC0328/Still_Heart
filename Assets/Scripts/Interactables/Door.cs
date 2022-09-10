using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private string nextRoom;
    [SerializeField]
    private bool isLocked = false;

    public override void Interact()
    {
        if (!isLocked)
            ChangeRoom();

        base.Interact();
    }

    private void ChangeRoom()
    {
        GameManager.Instance.ChangeScene(nextRoom);
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
