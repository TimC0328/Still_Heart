using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [SerializeField]
    private Text interactText;

    private int lineNum = 0;

    private IEnumerator typewriter;

    private bool display = false;
    private bool typing = false;

    private Interactable interactable = null;

    private List<string> lines;

    public void DisplayInteractText(Interactable interact, List<string> text)
    {
        interactable = interact;
        lines = text;

        lineNum = 0;
        typing = true;

        display = true;

        StartCoroutine(Typewriter(lines[0], interactText));
    }

    private void Update()
    {
        if (!display)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!typing)
            {
                if (lineNum < lines.Count)
                {
                    typing = true;
                    StartCoroutine(Typewriter(lines[lineNum], interactText));
                }
                else
                {
                    display = false;
                    interactText.text = "";
                    interactable.OnMessageEnd();
                }
            }
            else
                typing = false;
        }
    }

    private IEnumerator Typewriter(string text, Text uiText)
    {
        WaitForSeconds waitTimer = new WaitForSeconds(.05f);
        uiText.text = "";
        foreach (char c in text)
        {
            uiText.text = uiText.text + c;
            if(!typing)
            {
                uiText.text = text;
                break;
            }
            else
                yield return waitTimer;
        }
        typing = false;
        lineNum++;
    }

}
