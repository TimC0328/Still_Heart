using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [SerializeField]
    private Text interactText;

    public bool typing = false;

    private IEnumerator typewriter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayInteractText(string text)
    {
        if (typing)
            StopCoroutine(typewriter);

        if (text == "")
        {
            interactText.text = "";
            return;
        }

        typewriter = Typewriter(text, interactText);
        StartCoroutine(typewriter);
    }

    private IEnumerator Typewriter(string text, Text uiText)
    {
        WaitForSeconds waitTimer = new WaitForSeconds(.05f);
        uiText.text = "";
        typing = true;
        foreach (char c in text)
        {
            uiText.text = uiText.text + c;
            yield return waitTimer;
        }
        typing = false;
    }

}
