using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField]
    private Cutscene cutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (cutscene.cutsceneTriggered)
            return;

        if (other.tag == "Player")
        {
            Debug.Log("Player entered");

            GameObject.Find("Cutscene Manager").GetComponent<CutsceneManager>().LoadCutscene(cutscene);
        }

    }
}
