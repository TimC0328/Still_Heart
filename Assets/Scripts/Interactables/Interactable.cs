using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string interactName = "";
    [SerializeField]
    protected string[] interactMessage;
    protected bool canInteract;

    protected DisplayText interactUI;

    protected bool displayMessage = false;
    protected int lineNum = 0;

    protected virtual void Start()
    {
        interactUI = GameObject.Find("Canvas").GetComponent<DisplayText>();
    }

    protected virtual void Update()
    {
        if (displayMessage)
            InteractableMessage();
    }
    
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactName);
        GameManager.Instance.player.SetState(3);
        if (interactMessage.Length > 0)
        {
            displayMessage = true;
            interactUI.DisplayInteractText(interactMessage[lineNum]);
        }
    }

    protected virtual void InteractableMessage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lineNum++;
            if (lineNum < interactMessage.Length)
                interactUI.DisplayInteractText(interactMessage[lineNum]);
            else
                OnMessageEnd();
        }
    }

    protected virtual void OnMessageEnd()
    {
        lineNum--;
        interactUI.DisplayInteractText("");
        InteractionEnd(0);
    }

    protected virtual void InteractionEnd(int newState)
    {
        GameManager.Instance.player.SetState(newState);
    }
}
