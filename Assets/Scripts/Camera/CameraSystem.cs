using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Camera otsCamera;
    [SerializeField]
    private Camera fpsCamera;

    [SerializeField]
    private Camera inventoryCamera;

    private Camera cutsceneCamera;


    [SerializeField]
    private Canvas canvas;


    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        otsCamera = transform.GetChild(2).gameObject.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        otsCamera.enabled = false;
        mainCamera.enabled = true;
    }

    public void ToggleView()
    {
        otsCamera.enabled = !otsCamera.enabled;
        mainCamera.enabled = !mainCamera.enabled;
    }

    public void ToggleViewFPS()
    {
        fpsCamera.enabled = !fpsCamera.enabled;
        mainCamera.enabled = !mainCamera.enabled;
    }

    public void ChangeMainCamera(Camera newCam)
    {
        if (mainCamera == newCam)
            return;

        mainCamera.enabled = false;

        mainCamera = newCam;

        mainCamera.enabled = true;

        canvas.worldCamera = mainCamera;

    }

    public void InventoryCameraToggle()
    {
        inventoryCamera.gameObject.SetActive(!inventoryCamera.gameObject.activeSelf);
        if(inventoryCamera.gameObject.activeSelf)
            canvas.worldCamera = inventoryCamera;
        else
            canvas.worldCamera = mainCamera;
    }

    public void CutsceneCameraEnable(Camera cam)
    {
        cutsceneCamera = mainCamera;
        ChangeMainCamera(cam);
    }
    public void CutsceneCameraDisable()
    {
        ChangeMainCamera(cutsceneCamera);
        cutsceneCamera = null;
    }
}
