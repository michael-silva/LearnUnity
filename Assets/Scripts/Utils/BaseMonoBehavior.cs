using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class BaseMonoBehavior : MonoBehaviour
{
    void Awake()
    {
        InjectComponents(this);
    }
    private const BindingFlags bindingAttrs = BindingFlags.Instance |
                                              BindingFlags.Public |
                                              BindingFlags.NonPublic;

    static void InjectComponents(MonoBehaviour target)
    {
        Type targetType = target.GetType();
        FieldInfo[] fields = targetType.GetFields(bindingAttrs);

        Debug.Log($"Target type: {targetType}");

        foreach (FieldInfo fi in fields)
        {

            Debug.Log($"Field: {fi.Name}, type: {fi.FieldType}");

            IEnumerable<Attribute> attributes = fi.GetCustomAttributes();
            foreach (Attribute attribute in attributes)
            {
                if (attribute is not InjectOnAwakeAttribute)
                {
                    continue;
                }

                Debug.Log($"Attribute: {attribute.GetType()}");

                // Now we know that a field on the provided target class
                // has the FetchComponent attribute. Let's get the Component
                // and pass it to the target instance.

                Component component = target.gameObject.GetComponent(fi.FieldType);
                fi.SetValue(target, component);
            }
        }
    }
}
