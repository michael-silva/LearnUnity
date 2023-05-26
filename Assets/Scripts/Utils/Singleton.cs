using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; protected set; }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = (T)this;
    }
}
