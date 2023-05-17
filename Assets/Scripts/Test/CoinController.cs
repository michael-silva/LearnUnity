using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CoinController : BaseMonoBehavior
{
    [InjectOnAwake]
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start" + myRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);
    }
}
