using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    public Vector3 offset;
    public Vector2 size = Vector2.one;
    public Vector2Int grid;
    public BoxCollider boxColide;

    private void Start()
    {
        Vector3 c = Vector2.Scale(grid, size);
        c.z = c.y;
        c.y = 0;
        boxColide.center = (c + offset) * 0.5f;
        boxColide.size = c;
        boxColide.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 c = Vector2.Scale(grid, size);
        c.z = c.y;
        c.y = 0;
        Matrix4x4 mt = Matrix4x4.TRS(transform.TransformPoint((c + offset) * 0.5f), transform.rotation, Vector3.one);
        Gizmos.matrix = mt;
        Gizmos.DrawWireCube(Vector3.zero, c);
    }

    public Vector2Int WorldToCellPos(Vector3 pos)
    {
        Vector3 c = transform.InverseTransformPoint(pos);
        return new Vector2Int((int)((c.x - offset.x) / size.x), (int)((c.z - offset.z) / size.y));
    }

    public Vector3 CellToWorldPos(Vector2Int pos)
    {
        Vector3 c = new Vector3((pos.x * size.x) + offset.x + 0.5f, 0, (pos.y * size.y) + offset.z + 0.5f);
        return transform.TransformPoint(c);
    }
}
