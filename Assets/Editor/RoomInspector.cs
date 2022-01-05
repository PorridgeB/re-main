using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(RoomMesh))]
public class RoomInspector : Editor
{
    [MenuItem("GameObject/Room Mesh", false, 10)]
    private static void CreateRoomMesh(MenuCommand menuCommand)
    {
        var go = new GameObject("Room Mesh");

        go.tag = "Room";

        go.AddComponent<RoomMesh>();

        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var room = target as RoomMesh;

        if (GUILayout.Button("Rebuild"))
        {
            room.Rebuild();
        }

        GUILayout.Space(8f);

        if (GUILayout.Button("Open Room Editor"))
        {
            var window = (RoomEditorWindow)EditorWindow.GetWindow(typeof(RoomEditorWindow), false, "Room Editor");
            window.Show();
        }
    }
}
