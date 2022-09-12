using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDataManager : MonoBehaviour
{
    public RoomData roomData;

    private void Start()
    {
        GameManager.Instance.SetRoomManager(this);
    }

    public void LoadData()
    {
        Debug.Log("Loading room data");
        if (roomData.interactables == null)
            return;
        LoadInteractables(roomData.interactables);
    }

    private void LoadInteractables(InteractableData data)
    {
        Debug.Log("Loading interactable data");
        if (data.doors == null)
            return;
        Debug.Log("Loading doors");
        foreach (DoorObject doorData in data.doors)
        {
            Door door = GameObject.Find(doorData.name).GetComponent<Door>();
            Debug.Log("Loading door " + doorData.name);
            if (door == null)
            {
                Debug.Log("Error!" + doorData.name + " doesn't exist");
                continue;
            }
            Debug.Log("Found.");
            door.SetLocked(doorData.isLocked);
        }

    }
}
