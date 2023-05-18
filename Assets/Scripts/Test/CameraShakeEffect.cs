using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

public class CameraShakeEffect : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        var originalPos = transform.localPosition;
        float elapsed = 0f;

        Log.Debug(duration, magnitude);

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Log.Debug($"x[{x}], y[{y}]");

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Log.Debug($"reset");
        transform.localPosition = originalPos;
    }
}
