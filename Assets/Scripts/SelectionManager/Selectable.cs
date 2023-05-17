using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selectable : MonoBehaviour
{
    [SerializeField] private Transform selectionTransform;
    [SerializeField] private TextMeshProUGUI lookPercentageLabel;
    [HideInInspector] public float LookPercentage;

    void Awake()
    {
        if (!selectionTransform)
        {
            selectionTransform = transform;
        }
    }
    void Update()
    {
        lookPercentageLabel.text = LookPercentage.ToString("F3");
    }

    public Transform GetSelection()
    {
        return selectionTransform;
    }
}
