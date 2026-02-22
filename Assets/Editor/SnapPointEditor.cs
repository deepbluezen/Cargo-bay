using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public class SnapPointEditor
{
    static SnapPointEditor()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        if (Selection.activeTransform == null) return;
        var selected = Selection.activeTransform;
        var snapPointGroups = GameObject.FindObjectsOfType<SnapPointGroup>();

        foreach (var group in snapPointGroups)
        {
            foreach (var snapPoint in group.snapPoints)
            {
                if (snapPoint == null) continue;
                if (snapPoint == selected) continue;

                float dist = Vector3.Distance(selected.position, snapPoint.position);
                if (dist < group.snapRange)
                {
                    Handles.color = Color.green;
                    Handles.DrawLine(selected.position, snapPoint.position);
                    Handles.Label(snapPoint.position + Vector3.up * 0.7f, $"Snap ({dist:F2})");

                    // Auto-snap when in range
                    Undo.RecordObject(selected, "Snap to Point");
                    selected.position = snapPoint.position;
                    if (group.useOrientation)
                        selected.rotation = snapPoint.rotation;
                    break; // Only snap to the first found snap point in range
                }
            }
        }
    }
}