using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : BaseMonoBehavior
{
    [InjectOnAwake]
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(myRenderer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
