using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneManager : MonoBehaviour
{
    private Cutscene cutscene;

    [SerializeField]
    private Camera movieCam;

    private CutsceneEvent currentEvent;
    private int eventIndex = 0;
    private int numEvents = 0;

    [SerializeField]
    private GameObject ui;
    private Text dialogText;

    private bool typing = false;
    private int lineNum = 0;

    private void Start()
    {
        dialogText = ui.transform.GetChild(1).GetComponent<Text>();
    }

    public void LoadCutscene(Cutscene newCutscene)
    {
        cutscene = newCutscene;
        StartCutscene();
    }


    public void StartCutscene()
    {
        GameManager.Instance.player.SetState(4);
        ui.SetActive(true);

        eventIndex = 0;
        numEvents = cutscene.GetTotalEvents();

        GameManager.Instance.player.GetComponent<CameraSystem>().CutsceneCameraEnable(movieCam);

        PlayEvent();
    }

    private void PlayEvent()
    {
        currentEvent = cutscene.GetEvent(eventIndex);

        movieCam.transform.position = currentEvent.cameraData.cameraPos;
        movieCam.transform.rotation = Quaternion.Euler(currentEvent.cameraData.cameraAngle);

        lineNum = 0;

        typing = true;
        StartCoroutine(Typewriter(currentEvent.dialogLines[lineNum]));
    }

    private void NextEvent()
    {
        eventIndex++;

        if (eventIndex >= numEvents)
        {
            EndCutscene();
            return;
        }

        PlayEvent();
        
    }

    private void EndCutscene()
    {
        ui.SetActive(false);
        cutscene.cutsceneTriggered = true;
        GameManager.Instance.player.GetComponent<CameraSystem>().CutsceneCameraDisable();
        GameManager.Instance.player.SetState(0);

    }

    private void Update()
    {
        if (!ui.activeSelf)
            return;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!typing)
            {
                if (lineNum < currentEvent.dialogLines.Length)
                {
                    typing = true;
                    StartCoroutine(Typewriter(currentEvent.dialogLines[lineNum]));
                }
                else
                    NextEvent();
            }
            else
                typing = false;
        }
    }

    private IEnumerator Typewriter(string text)
    {
        WaitForSeconds waitTimer = new WaitForSeconds(.05f);
        dialogText.text = "";
        foreach (char c in text)
        {
            dialogText.text = dialogText.text + c;
            if (!typing)
            {
                dialogText.text = text;
                break;
            }
            else
                yield return waitTimer;
        }
        typing = false;
        lineNum++;
    }
}
