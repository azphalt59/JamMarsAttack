using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TaskCore))]
public class TaskCoreEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Find Task Triggers"))
        {
            TaskCore.Instance.AddAll(GameObject.FindObjectsOfType<HoldKeyTask>());
        }
    }
}