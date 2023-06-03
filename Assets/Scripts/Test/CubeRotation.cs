using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public Quaternion rotation;

    public Vector3 eulerAngles;

    public Transform target;
    public Transform orbit;
    public float orbitRadius = 1;
    public Transform origin;
    private float originRotation = 0;

    void Start()
    {
        if (orbit)
        {
            transform.position = orbit.position - (transform.forward * orbitRadius);
        }

        if (origin)
        {
            var offset = Quaternion.Euler(0, 0, 0) * (origin.forward * orbitRadius);
            transform.position = origin.position + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.rotation;
        eulerAngles = transform.eulerAngles;

        if (target)
        {

            var direction = target.position - transform.position;
            // Look direct at target.position don't work
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (orbit)
        {
            transform.RotateAround(orbit.position, Vector3.up, 50 * Time.deltaTime);
        }
        if (origin)
        {
            originRotation += 50 * Time.deltaTime;
            var offset = Quaternion.Euler(0, originRotation, 0) * (origin.forward * orbitRadius);
            transform.position = origin.position + offset;
        }
    }
}
