using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class AlignToGrid : MonoBehaviour
{
#if (UNITY_EDITOR)
    public int grid = 32;
    public Vector3 offset;

    private void Update()
    {
        if (EditorApplication.isPlaying) Destroy(this);

        Vector3 p = transform.localPosition;
        transform.localPosition = new Vector3(ToGridPos(p.x) + GetOffset(offset.x), ToGridPos(p.y) + GetOffset(offset.y), ToGridPos(p.z) + GetOffset(offset.z));
    }

    float ToGridPos(float pos)
    {
        return Mathf.Round(pos * grid) / grid;
    }
    float GetOffset(float offset)
    {
        return (1f / (float)grid * offset);
    }
#endif
}
