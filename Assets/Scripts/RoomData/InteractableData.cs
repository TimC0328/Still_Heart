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
    public string name;     //Refers to the GAMEOBJECT NAME
    public int state;     // Whether the door is broken, locked, or unlocked (-1,0,1) 
}