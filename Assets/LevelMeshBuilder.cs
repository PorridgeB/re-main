using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMeshBuilder : MonoBehaviour
{
    public Tilemap WallsTilemap;
    public Tilemap FloorsTilemap;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        foreach (var cell in FloorsTilemap.cellBounds.allPositionsWithin)
        {
            var tile = FloorsTilemap.GetTile(cell);
            
            if (tile != null)
            {
                var floorSprite = FloorsTilemap.GetSprite(cell);

                uvs.AddRange(floorSprite.uv);

                Debug.Log(floorSprite.uv[0]);

                var firstVertexIndex = vertices.Count;

                vertices.Add(new Vector3(cell.x, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y + 1));
                vertices.Add(new Vector3(cell.x, 0, cell.y + 1));

                AddQuad(firstVertexIndex, triangles);
            }
        }

        //foreach (var cell in WallsTilemap.cellBounds.allPositionsWithin)
        //{
        //    var tile = FloorsTilemap.GetTile(cell);

        //    if (tile != null)
        //    {
        //        var floorSprite = FloorsTilemap.GetSprite(cell);

        //        uvs.AddRange(floorSprite.uv);

        //        Debug.Log(floorSprite.uv[0]);

        //        var firstVertexIndex = vertices.Count;

        //        vertices.Add(new Vector3(cell.x, 0, cell.y));
        //        vertices.Add(new Vector3(cell.x + 1, 0, cell.y));
        //        vertices.Add(new Vector3(cell.x + 1, 0, cell.y + 1));
        //        vertices.Add(new Vector3(cell.x, 0, cell.y + 1));

        //        AddQuad(firstVertexIndex, triangles);
        //    }
        //}

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        mesh.Optimize();

        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;
    }

    void AddQuad(int firstVertexIndex, List<int> triangles)
    {
        triangles.Add(firstVertexIndex);
        triangles.Add(firstVertexIndex + 2);
        triangles.Add(firstVertexIndex + 1);

        triangles.Add(firstVertexIndex);
        triangles.Add(firstVertexIndex + 3);
        triangles.Add(firstVertexIndex + 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
