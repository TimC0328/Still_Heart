using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!cam.enabled)
            return;

        transform.LookAt(target, Vector3.up);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
