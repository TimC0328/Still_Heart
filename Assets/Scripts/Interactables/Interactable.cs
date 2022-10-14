using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string interactName = "";
    [SerializeField]
    protected List<string> text;
    protected bool canInteract;

    protected DisplayText interactUI;

    protected bool displayMessage = false;

    protected virtual void Start()
    {
        interactUI = GameObject.Find("Canvas").GetComponent<DisplayText>();
    }

    
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactName);
        GameManager.Instance.player.SetState(3);
        interactUI.DisplayInteractText(this, text);
    }

    public virtual void OnMessageEnd()
    {
        InteractionEnd(0);
    }

    protected virtual void InteractionEnd(int newState)
    {
        GameManager.Instance.player.SetState(newState);
    }
}
