using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Cutscene", menuName = "Cutscene")]
public class Cutscene : ScriptableObject
{
    [SerializeField]
    private CutsceneEvent[] events;

    public GameObject[] actors;

    public bool cutsceneTriggered;

    public CutsceneEvent GetEvent(int index)
    {
        return events[index];
    }

    public int GetTotalEvents()
    {
        return events.Length;
    }
}

[Serializable]
public struct CutsceneEvent
{
    public string[] dialogLines;
    public bool KeepLastAngle;
    public CameraData cameraData;
}

[Serializable]
public struct CameraData
{
    public Vector3 cameraPos;
    public Vector3 cameraAngle;
}