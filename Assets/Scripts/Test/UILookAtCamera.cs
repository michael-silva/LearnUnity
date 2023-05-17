using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    void LateUpdate()
    {
        var camRotation = Camera.main.transform.rotation;
        // transform.LookAt(transform.position + camRotation * Vector3.forward, camRotation * Vector3.up);
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180, 0);
    }
}
