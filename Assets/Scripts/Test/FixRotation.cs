using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    private Quaternion initialEulerAngles;
    // Start is called before the first frame update
    void Awake()
    {
        initialEulerAngles = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = initialEulerAngles;
    }
}
