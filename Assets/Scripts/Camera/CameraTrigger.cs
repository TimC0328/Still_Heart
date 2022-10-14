using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered");

            other.transform.gameObject.GetComponent<CameraSystem>().ChangeMainCamera(cam);
        }

    }
}
