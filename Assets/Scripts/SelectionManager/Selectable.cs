using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selectable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lookPercentageLabel;
    [HideInInspector] public float LookPercentage;


    // Update is called once per frame
    void Update()
    {
        lookPercentageLabel.text = LookPercentage.ToString("F3");
    }
}
