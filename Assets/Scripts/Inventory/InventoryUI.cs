using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private enum Modes { DEFAULT, EQUIP, INSPECT, COMBINE};
    private Modes mode;

    private Inventory inventory;

    private Item selectedItem;

    [SerializeField]
    private Transform displayItem;
    private GameObject currentModel;


    [SerializeField]
    private Transform slots;

    [SerializeField]
    private Text caption;
    [SerializeField]
    private Text inspect;

    [SerializeField]
    private GameObject buttons;

    private List<string> lines;
    private int lineNum;
    private bool typing;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        //inventory.inventory[0].DisplayModel(displayItem);
    }

    public void SelectSlot(int slot)
    {
        Debug.Log("Clicked slot " + slot);
        if (inventory.inventory.Count <= slot)
            return;

        if (selectedItem == inventory.inventory[slot])
            return;

        if(mode == Modes.COMBINE)
        {
            if (selectedItem.Combine(inventory.inventory[slot]))
                CombineResult("Combined the " + selectedItem.GetItemName() + " with " + inventory.inventory[slot].GetItemName());
            else
                CombineResult("Cannot combine.");
            return;
        }

        if (currentModel)
            Destroy(currentModel);

        selectedItem = inventory.inventory[slot];

        currentModel = selectedItem.DisplayModel(displayItem);

        caption.text = selectedItem.GetItemName();

    }

    public void ItemMenu(int selection)
    {
        if (!selectedItem)
            return;

        switch(selection)
        {
            case 0:
                Debug.Log("Use!");
                if (!selectedItem.Equip())
                    Debug.Log("Cannot equip! Using instead.");
                else
                    EquipText();
                break;
            case 1:
                Debug.Log("Inspect!");
                InspectCurrentItem();
                break;
            case 2:
                Debug.Log("Combine!");
                StartCombine();
                break;
            default:
                break;
        }
    }

    private void UseCurrentItem()
    {
        GameManager.Instance.player.GetComponent<CameraSystem>().InventoryCameraToggle();
        gameObject.SetActive(false);
    }

    private void InspectCurrentItem()
    {
        slots.gameObject.SetActive(false);
        buttons.SetActive(false);

        inspect.enabled = true;

        lines = selectedItem.Inspect();

        lineNum = 0;

        UpdateInspectText();

        mode = Modes.INSPECT;
    }

    private void EndInspect()
    {
        slots.gameObject.SetActive(true);
        buttons.SetActive(true);

        inspect.enabled = false;


        mode = Modes.DEFAULT;

    }

    private void StartCombine()
    {
        caption.text = "Combine:";
        mode = Modes.COMBINE;
    }

    private void CombineResult(string text)
    {
        slots.gameObject.SetActive(false);
        buttons.SetActive(false);

        inspect.enabled = true;

        typing = true;
        StartCoroutine(Typewriter(text));

        mode = Modes.EQUIP;
    }

    private void Update()
    {
        switch(mode)
        {
            case Modes.DEFAULT:
                break;
            case Modes.EQUIP:
                if (Input.GetKeyDown(KeyCode.Space))
                    UpdateEquipText();
                break;
            case Modes.INSPECT:
                if (Input.GetKeyDown(KeyCode.Space))
                    UpdateInspectText();
                break;
            case Modes.COMBINE:
                break;

        }
    }

    private void UpdateInspectText()
    {
        if (!typing)
        {
            if (lineNum < lines.Count)
            {
                typing = true;
                StartCoroutine(Typewriter(lines[lineNum]));
            }
            else
                EndInspect();
        }
        else
            typing = false;
    }

    private void EquipText()
    {
        string text = selectedItem.GetItemName() + " has been equipped.";

        slots.gameObject.SetActive(false);
        buttons.SetActive(false);

        inspect.enabled = true;

        typing = true;
        StartCoroutine(Typewriter(text));

        mode = Modes.EQUIP;
    }

    private void UpdateEquipText()
    {
        if (!typing)
            EndInspect();
        else
            typing = false;
    }

    private IEnumerator Typewriter(string text)
    {
        WaitForSeconds waitTimer = new WaitForSeconds(.05f);
        inspect.text = "";
        foreach (char c in text)
        {
            inspect.text = inspect.text + c;
            if (!typing)
            {
                inspect.text = text;
                break;
            }
            else
                yield return waitTimer;
        }
        typing = false;
        lineNum++;
    }
}
