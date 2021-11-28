using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMeshBuilder : MonoBehaviour
{
    public Tilemap WallsTilemap;
    public Tilemap FloorsTilemap;
    
    void Start()
    {
        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = GenerateMesh();

        var meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = GenerateCollisionMesh();
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

    Mesh GenerateMesh()
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

                vertices.Add(new Vector3(cell.x + 1, 1, cell.y));
                vertices.Add(new Vector3(cell.x, 1, cell.y));
                vertices.Add(new Vector3(cell.x, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y));

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

                //uvs.Add(new Vector2());
                //uvs.Add(new Vector2());
                //uvs.Add(new Vector2());
                //uvs.Add(new Vector2());

                //vertices.Add(new Vector3(cell.x, 1, cell.y));
                //vertices.Add(new Vector3(cell.x + 1, 1, cell.y));
                //vertices.Add(new Vector3(cell.x + 1, 1, cell.y + 1));
                //vertices.Add(new Vector3(cell.x, 1, cell.y + 1));

                //AddQuad(firstVertexIndex, triangles);
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

        return mesh;
    }

    Mesh GenerateCollisionMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        foreach (var cell in FloorsTilemap.cellBounds.allPositionsWithin)
        {
            var tile = FloorsTilemap.GetTile(cell);

            if (tile != null)
            {
                var firstVertexIndex = vertices.Count;

                vertices.Add(new Vector3(cell.x, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y + 1));
                vertices.Add(new Vector3(cell.x, 0, cell.y + 1));

                AddQuad(firstVertexIndex, triangles);

                // Generate collision mesh for adjacent walls
                Vector3[][] wallVertexOffsets = new Vector3[][]
                {
                    new Vector3[] { new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 0) },
                    new Vector3[] { new Vector3(1, 1, 0), new Vector3(1, 1, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0) },
                    new Vector3[] { new Vector3(1, 1, 1), new Vector3(0, 1, 1), new Vector3(0, 0, 1), new Vector3(1, 0, 1) },
                    new Vector3[] { new Vector3(0, 1, 1), new Vector3(0, 1, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 1) }
                };

                Vector3Int[] wallOffsets = new Vector3Int[] { new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0) };

                for (int i = 0; i < wallOffsets.Length; i++)
                {
                    Vector3Int wallOffset = wallOffsets[i];

                    if (!FloorsTilemap.HasTile(cell + wallOffset))
                    {
                        firstVertexIndex = vertices.Count;

                        Vector3 worldPosition = new Vector3(cell.x, 0, cell.y);

                        foreach (Vector3 wallVertexOffset in wallVertexOffsets[i])
                        {
                            vertices.Add(worldPosition + wallVertexOffset);
                        }

                        AddQuad(firstVertexIndex, triangles);
                    }
                }
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

        return mesh;
    }

    void Update()
    {
        
    }
}
