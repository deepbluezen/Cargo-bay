using UnityEngine;
using System.Collections.Generic;

public class SnapPointGroup : MonoBehaviour
{
    public float snapRange = 1.0f;
    public bool useOrientation = true;
    public List<Transform> snapPoints = new List<Transform>();

    // Show gizmos for all snap points within range of the selected object, and always for the selected object's own snap points
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        var selected = UnityEditor.Selection.activeTransform;
        if (selected == null) return;

        foreach (var t in snapPoints)
        {
            if (t != null)
            {
                bool isSelectedGroup = selected == this.transform;
                float dist = Vector3.Distance(selected.position, t.position);

                // Show if this is the selected object's snap point, or if within range of the selected object
                if (isSelectedGroup || dist < snapRange)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(t.position, snapRange);
                    UnityEditor.Handles.Label(t.position + Vector3.up * 0.2f, "Snap");
                    if (useOrientation)
                    {
                        UnityEditor.Handles.ArrowHandleCap(0, t.position, t.rotation, 0.5f, EventType.Repaint);
                    }
                }
            }
        }
#endif
    }
}