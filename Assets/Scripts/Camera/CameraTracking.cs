using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    private Transform player;
    private Camera cam;

    private void Start()
    {
        player = GameManager.Instance.player.gameObject.transform;
        cam = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (!cam.enabled)
            return;

        transform.LookAt(player, Vector3.up);
    }
}
