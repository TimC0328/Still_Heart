using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_Data", menuName = "Room Data")]
public class RoomData : ScriptableObject
{
    public InteractableData interactables;
}
