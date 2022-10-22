using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameObjectOfTypeAttribute))]
public class GameObjectOfTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool isFieldGameObject = IsFieldGameObject();

        if (!isFieldGameObject)
        {
            DrawError(position);
            return;
        }

        var gootAttribute = attribute as GameObjectOfTypeAttribute;
        var requareType = gootAttribute.Type;

        CheckDragAndDrops(position, requareType);
        CheckValue(property, requareType);
        DrowObjectField(property, position, label,
            gootAttribute.AllowSceneObject, gootAttribute.Type);
    }

    private void CheckValue(SerializedProperty property, Type requiredType)
    {
        if (property.objectReferenceValue != null)
        {
            if (!IsValidObject(property.objectReferenceValue, requiredType))
            {
                property.objectReferenceValue = null;
            }
        }
    }

    private void CheckDragAndDrops(Rect position, Type requareType)
    {
        if (position.Contains(Event.current.mousePosition))
        {
            var dragerObjectCount = DragAndDrop.objectReferences.Length;
            for (int i = 0; i < dragerObjectCount; i++)
            {
                if (!IsValidObject(DragAndDrop.objectReferences[i], requareType))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                    break;
                }
            }
        }
    }

    private bool IsValidObject(UnityEngine.Object o, Type requareType)
    {
        bool result = false;

        var go = o as GameObject;
        if (go != null)
        {
            result = go.GetComponent(requareType) != null;
        }
        return result;
    }

    private void DrawError(Rect position)
    {
        EditorGUI.HelpBox(position,
            $"GameObjectOfTypeAttribute works onle with Gameobject references", MessageType.Error);
    }

    private bool IsFieldGameObject()
    {
        return fieldInfo.FieldType == typeof(GameObject) ||
            typeof(IEnumerable<GameObject>).IsAssignableFrom(fieldInfo.FieldType);
    }

    private void DrowObjectField(SerializedProperty property, Rect position,
        GUIContent lablle,
        bool allowSceneObject,
        Type typeIterface)
    {
        lablle.text = lablle.text + " (" + typeIterface.Name + ")";
        property.objectReferenceValue = EditorGUI.ObjectField(position
            , lablle
            , property.objectReferenceValue
            , typeof(GameObject),
            allowSceneObject);
    }
}

