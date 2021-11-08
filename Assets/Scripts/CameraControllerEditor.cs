using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controlType"));

        switch ((ControlType)serializedObject.FindProperty("controlType").intValue)
        {
            case ControlType.SlowFollow:
                DisplaySlowFollow();
                break;
            case ControlType.Follow:
                DisplayFollow();
                break;
            case ControlType.Input:
                DisplayInput();
                break;
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bindingType"));

        switch ((BindingType)serializedObject.FindProperty("bindingType").intValue)
        {
            case BindingType.Sprite:
                DisplayGameObject();
                break;
            case BindingType.Values:
                DisplayValues();
                break;
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("size"));

        serializedObject.ApplyModifiedProperties();
    }

    void DisplayInput()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
    }

    void DisplaySlowFollow()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("minSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSpeed"));
    }

    void DisplayFollow()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));
    }

    void DisplayGameObject()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("background"));
    }

    void DisplayValues()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("min"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("max"));
    }
}
