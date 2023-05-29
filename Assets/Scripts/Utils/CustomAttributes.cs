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

public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}