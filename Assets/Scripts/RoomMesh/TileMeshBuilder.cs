using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeshBuilder
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uv = new List<Vector2>();

    public void AddTile(Vector3 position, Vector2Int size, Vector3Int normal, Vector2[] textureUvs)
    {
        AddTile(position, size, normal);

        var uvs = textureUvs != null ? textureUvs : new Vector2[] { new Vector2(), new Vector2(), new Vector2(), new Vector2() };

        uv.AddRange(uvs);
    }

    public void AddTile(Vector3 position, Vector2Int size, Vector3Int normal)
    {
        var rotation = Quaternion.FromToRotation(Vector3.up, normal);

        var baseVertices = new Vector3[] { new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 0, 0) };
        var baseTriangles = new int[] { 2, 0, 1, 2, 1, 3 };

        var firstVertexIndex = vertices.Count;
        vertices.AddRange(baseVertices.Select(x => (rotation * new Vector3(x.x * size.x, 0, x.z * size.y)) + position));
        triangles.AddRange(baseTriangles.Select(x => x + firstVertexIndex));
    }

    public Mesh ToMesh()
    {
        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();

        return mesh;
    }
}
