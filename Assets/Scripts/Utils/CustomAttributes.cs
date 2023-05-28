using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.Scripting;
using System.Reflection;

public enum InjectType
{
    Self,
    Parent,
    Child,
}
public class InjectOnAwakeAttribute : Attribute
{
    public InjectOnAwakeAttribute(InjectType injectType = InjectType.Self)
    {
        this.InjectType = injectType;
    }

    public InjectType InjectType { get; private set; }
}

