using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMeshBuilder : MonoBehaviour
{
    public Tilemap WallsTilemap;
    public Tilemap FloorsTilemap;

    // TODO: Generate collision mesh
    // TODO: Generate navmesh

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        // Generate floor mesh
        foreach (var cell in FloorsTilemap.cellBounds.allPositionsWithin)
        {
            var tile = FloorsTilemap.GetTile(cell);
            
            if (tile != null)
            {
                var floorSprite = FloorsTilemap.GetSprite(cell);

                // Stupid hack to set the render the mesh with the
                // same texture as the tilemap.
                GetComponent<MeshRenderer>().material.mainTexture = floorSprite.texture;

                uvs.AddRange(floorSprite.uv);

                var firstVertexIndex = vertices.Count;

                vertices.Add(new Vector3(cell.x, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y + 1));
                vertices.Add(new Vector3(cell.x, 0, cell.y + 1));

                AddQuad(firstVertexIndex, triangles);
            }
        }

        // Generate wall mesh
        foreach (var cell in WallsTilemap.cellBounds.allPositionsWithin)
        {
            var tile = WallsTilemap.GetTile(cell);

            if (tile != null)
            {
                var wallSprite = WallsTilemap.GetSprite(cell);

                uvs.AddRange(wallSprite.uv);

                var firstVertexIndex = vertices.Count;

                vertices.Add(new Vector3(cell.x + 1, 1, cell.y + 1));
                vertices.Add(new Vector3(cell.x, 1, cell.y + 1));
                vertices.Add(new Vector3(cell.x, 0, cell.y + 1));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y + 1));

                AddQuad(firstVertexIndex, triangles);
            }
        }

        // Generate wall trimmings mesh
        foreach (var cell in FloorsTilemap.cellBounds.allPositionsWithin)
        {
            var tile = FloorsTilemap.GetTile(cell);

            if (tile == null)
            {
                var edge = false;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x != 0 || y != 0)
                        {
                            if (FloorsTilemap.HasTile(cell + new Vector3Int(x, y, 0)))
                            {
                                edge = true;
                                break;
                            }
                        }
                    }
                }

                if (!edge)
                {
                    continue;
                }

                var firstVertexIndex = vertices.Count;

                uvs.Add(new Vector2());
                uvs.Add(new Vector2());
                uvs.Add(new Vector2());
                uvs.Add(new Vector2());

                vertices.Add(new Vector3(cell.x, 1, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 1, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 1, cell.y + 1));
                vertices.Add(new Vector3(cell.x, 1, cell.y + 1));

                AddQuad(firstVertexIndex, triangles);
            }
        }

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
