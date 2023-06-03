using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtils
{
    public static Vector3 ConvertToCameraSpace(Vector3 vector)
    {
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        // remove the Y values to ignore up/down direction
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        var cameraForwardProduct = vector.z * cameraForward;
        var cameraRightProduct = vector.x * cameraRight;

        var result = cameraForwardProduct + cameraRightProduct;
        result.y = vector.y;
        return result;

    }
}