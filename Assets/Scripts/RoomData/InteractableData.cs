using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InteractableData
{
    public DoorObject[] doors;
}

[Serializable]
public struct DoorObject
{
    [Tooltip("Refers to the GAMEOBJECT NAME")]
    public string name;
    [Tooltip("Whether the door is locked or not")]
    public bool isLocked;    
}