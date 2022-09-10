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
        foreach (DoorObject door in data.doors)
        {
            Door currentDoor = GameObject.Find(door.name).GetComponent<Door>();
            Debug.Log("Loading door " + door.name);
            if (currentDoor == null)
            {
                Debug.Log("Error!" + door.name + " doesn't exist");
                continue;
            }
            Debug.Log("Found.");
            switch(door.state)
            {
                case -1: //broken
                    break;
                case 0: //locked
                    break;
                case 1: //unlocked
                    currentDoor.Unlock();
                    break;
                default:
                    break;
            }
        }

    }
}
