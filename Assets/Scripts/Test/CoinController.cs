using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

[RequireComponent(typeof(Renderer))]
public class CoinController : BaseMonoBehavior
{
    [Header("Header here")]
    [SerializeField, Range(0.0f, 5.0f)]
    private float speedModifier = 1f;

    [Space]
    [SerializeField, Tooltip("Name for logging")]
    private string coinName;

    [InjectOnAwake]
    private Renderer myRenderer;

    // Update is called once per frame
    void Update()
    {
        // Log.Info($"Coin {coinName}", transform.rotation);
        transform.Rotate(new Vector3(0, 0, 90) * speedModifier * Time.deltaTime);
    }
}
