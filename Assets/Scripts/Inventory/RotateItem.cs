using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 20f * Time.deltaTime, 0f, Space.Self);
    }
}
