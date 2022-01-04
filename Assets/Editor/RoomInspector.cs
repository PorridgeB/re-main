using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(RoomMesh))]
public class RoomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var room = target as RoomMesh;

        if (GUILayout.Button("Rebuild"))
        {
            room.Rebuild();
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Move To Origin"))
        {
            room.MoveToOrigin();
            room.Rebuild();
        }
        if (GUILayout.Button("Clear"))
        {
            room.Clear();
            room.Rebuild();
        }
        GUILayout.EndHorizontal();
    }
}
