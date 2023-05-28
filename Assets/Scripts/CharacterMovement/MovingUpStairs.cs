using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MovingUpStairs
{

    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.15f;


    public void DrawGizmos(Transform transform)
    {
        float rayDistance = 0.7f;
        Gizmos.color = Color.green;
        var stepPosition = transform.position + Vector3.up * stepHeight;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);
        Gizmos.DrawLine(stepPosition, stepPosition + transform.forward * rayDistance);
    }

    public void MoveStairs(Transform transform, Rigidbody rigidBody)
    {
        bool isMovedUp = false;
        float rayDistance = 0.7f;
        if (!isMovedUp && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit1, rayDistance))
        {
            if (!Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(Vector3.forward), rayDistance))
            {
                // Debug.Log("1 -" + hit1.point);
                if (Physics.Raycast(transform.position + Vector3.up * 0.05f, transform.TransformDirection(Vector3.forward), out var hit2, rayDistance))
                {
                    // Debug.Log("2 - " + hit2.point);
                    if (hit1.point.x == hit2.point.x && hit1.point.z == hit2.point.z)
                    {
                        Debug.Log(rigidBody.position);
                        rigidBody.MovePosition(rigidBody.position + new Vector3(0, stepSmooth, 0));
                    }
                }
            }
        }

        // RaycastHit hitLower45;
        // if (!isMovedUp && Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        // {
        //     Debug.Log("hitLower 45");
        //     RaycastHit hitUpper45;
        //     if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
        //     {
        //         Debug.Log("up");
        //         rigidBody.position -= new Vector3(0, -stepSmooth, 0);
        //         isMovedUp = true;
        //     }
        // }

        // RaycastHit hitLowerMinus45;
        // if (!isMovedUp && Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        // {
        //     Debug.Log("hitLower -45");
        //     RaycastHit hitUpperMinus45;
        //     if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
        //     {
        //         Debug.Log("up");
        //         rigidBody.position -= new Vector3(0, -stepSmooth, 0);
        //         isMovedUp = true;
        //     }
        // }

    }
    public void MoveStairs(Transform transform)
    {
        bool isMovedUp = false;
        float rayDistance = 0.7f;
        if (!isMovedUp && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit1, rayDistance))
        {
            if (!Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(Vector3.forward), rayDistance))
            {
                // Debug.Log("1 -" + hit1.point);
                if (Physics.Raycast(transform.position + Vector3.up * 0.05f, transform.TransformDirection(Vector3.forward), out var hit2, rayDistance))
                {
                    // Debug.Log("2 - " + hit2.point);
                    if (hit1.point.x == hit2.point.x && hit1.point.z == hit2.point.z)
                    {
                        Debug.Log("Step UP");
                        transform.position += new Vector3(0, stepHeight * Time.deltaTime, 0);
                    }
                }
            }
        }
    }
}
