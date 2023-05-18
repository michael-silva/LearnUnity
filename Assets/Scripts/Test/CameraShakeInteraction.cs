using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

public class CameraShakeInteraction : MonoBehaviour, IInteraction
{
    [Header("Shake Effect Config")]
    [SerializeField, Range(0.1f, 10f)]
    private float duration = 0.5f;
    [SerializeField, Range(0.1f, 5f)]
    private float magnitude = 0.5f;

    private CameraShakeEffect cameraEffect;

    private void Awake()
    {
        cameraEffect = Camera.main.GetComponent<CameraShakeEffect>();
    }

    public void Interact()
    {
        StartCoroutine(cameraEffect.Shake(duration, magnitude));
    }
}
