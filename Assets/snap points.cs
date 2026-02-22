using UnityEngine;

[ExecuteInEditMode]
public class SnapPoint : MonoBehaviour
{
    public float snapRange = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, snapRange);
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, "Snap Point");
    }
}
