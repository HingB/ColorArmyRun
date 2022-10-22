using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectOfTypeAttribute : PropertyAttribute
{
    public Type Type { get; }
    public bool AllowSceneObject { get;}

    public GameObjectOfTypeAttribute(Type type, bool allowSceneObject= true)
    {
        Type = type;
        AllowSceneObject = allowSceneObject;
    }
}
