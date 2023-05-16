using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu is what will let us create the index asset in the project:
[CreateAssetMenu(menuName = nameof(ResourcesIndex), fileName = nameof(ResourcesIndex))]
public sealed class ResourcesIndex : BaseIndex
{
    // The static instance is what allows us to get the index from anywhere in the code:
    private static ResourcesIndex _instance;
    public static ResourcesIndex instance => GetOrLoad(ref _instance);

    // Set up your references below! 
    // You only need to assign references once with this pattern.
    public GameObject coinPrefab;
}

public abstract class BaseIndex : ScriptableObject
{
    protected static T GetOrLoad<T>(ref T _instance) where T : BaseIndex
    {
        if (_instance == null)
        {
            var name = typeof(T).Name;

            _instance = Resources.Load<T>(name);

            if (_instance == null)
            {
                Debug.LogWarning($"Failed to load index: '{name}'.\nIndex file must be placed at: Resources/{name}.asset");
            }
        }

        return _instance;
    }
}