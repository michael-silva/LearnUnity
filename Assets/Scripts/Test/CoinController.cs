using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

[RequireComponent(typeof(Renderer))]
public class CoinController : BaseMonoBehavior
{
    [InjectOnAwake]
    private Renderer myRenderer;

    // Update is called once per frame
    void Update()
    {
        Log.Info("Coin rotation", transform.rotation);
        transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);
    }
}
