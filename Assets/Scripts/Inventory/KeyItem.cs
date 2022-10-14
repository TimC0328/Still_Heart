using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key_Item", menuName = "Key Item")]
public class KeyItem : Item
{
    [SerializeField]
    private List<string> useText;

    public List<string> GetUseText()
    {
        return useText;
    }
}
