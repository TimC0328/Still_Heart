using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private Cutscene cutscene;

    [SerializeField]
    private Camera movieCam;

    private CutsceneEvent currentEvent;
    private int eventIndex = 0;

    [SerializeField]
    private GameObject ui;
    private Text dialogText;

    private bool typing = false;
    private int lineNum = 0;

    private void Start()
    {
        dialogText = ui.transform.GetChild(1).GetComponent<Text>();
        StartCutscene();
    }


    public void StartCutscene()
    {
        GameManager.Instance.player.SetState(4);
        ui.SetActive(true);

        eventIndex = 0;

        GameManager.Instance.player.GetComponent<CameraSystem>().ChangeMainCamera(movieCam);

        PlayEvent();
    }

    private void PlayEvent()
    {
        currentEvent = cutscene.GetEvent(eventIndex);
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
                {
                    Destroy(gameObject);
                }
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
