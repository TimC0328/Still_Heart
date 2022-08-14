using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Camera otsCamera;


    // Start is called before the first frame update
    void Start()
    {
        otsCamera.enabled = false;
        mainCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleView()
    {
        otsCamera.enabled = !otsCamera.enabled;
        mainCamera.enabled = !mainCamera.enabled;
    }

    public Camera ChangeMainCamera(Camera newCam)
    {
        mainCamera.enabled = false;

        Camera currentCam = mainCamera;
        mainCamera = newCam;

        mainCamera.enabled = true;

        return currentCam;
    }
}
