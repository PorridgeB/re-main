using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.AI.Navigation;

public class LevelMeshBuilder : MonoBehaviour
{
    public Tilemap WallTrimmingsTilemap;
    public Tilemap WallsTilemap;
    public Tilemap FloorsTilemap;

    // TODO: Collision on walls and wall trimmings only
    // TODO: Clean up
    // TODO: Make more robust
    // TODO: Tiles with different sizes
    // TODO: Pixelated lighting
    // TODO: AO

    void Start()
    {
        // Generate the visual mesh
        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = GenerateMesh();

        // Generate the collision mesh
        var meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = GenerateCollisionMesh();

        // Bake the navmesh
        var navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
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

                // Stupid hack to set the material with the
                // same texture as the tilemap.
                GetComponent<MeshRenderer>().material.mainTexture = floorSprite.texture;

                // Have to reorder the floor texture UVs for some reason
                var uv1 = floorSprite.uv[0];
                var uv2 = floorSprite.uv[1];
                var uv3 = floorSprite.uv[2];
                var uv4 = floorSprite.uv[3];

                uvs.Add(uv3);
                uvs.Add(uv4);
                uvs.Add(uv2);
                uvs.Add(uv1);

                //uvs.AddRange(floorSprite.uv);

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

                // Have to reorder the wall texture UVs for some reason
                var uv1 = wallSprite.uv[0];
                var uv2 = wallSprite.uv[1];
                var uv3 = wallSprite.uv[2];
                var uv4 = wallSprite.uv[3];

                uvs.Add(uv2);
                uvs.Add(uv1);
                uvs.Add(uv3);
                uvs.Add(uv4);

                var firstVertexIndex = vertices.Count;

                vertices.Add(new Vector3(cell.x + 1, 1, cell.y));
                vertices.Add(new Vector3(cell.x, 1, cell.y));
                vertices.Add(new Vector3(cell.x, 0, cell.y));
                vertices.Add(new Vector3(cell.x + 1, 0, cell.y));

                AddQuad(firstVertexIndex, triangles);
            }
        }

        // Generate wall trimmings mesh
        foreach (var cell in WallTrimmingsTilemap.cellBounds.allPositionsWithin)
        {
            var tile = WallTrimmingsTilemap.GetTile(cell);

            if (tile != null)
            {
                var wallTrimmingsSprite = WallTrimmingsTilemap.GetSprite(cell);

                // Have to reorder the floor texture UVs for some reason
                var uv1 = wallTrimmingsSprite.uv[0];
                var uv2 = wallTrimmingsSprite.uv[1];
                var uv3 = wallTrimmingsSprite.uv[2];
                var uv4 = wallTrimmingsSprite.uv[3];

                uvs.Add(uv3);
                uvs.Add(uv4);
                uvs.Add(uv2);
                uvs.Add(uv1);

                var firstVertexIndex = vertices.Count;

                vertices.Add(new Vector3(cell.x, 1, cell.y - 1));
                vertices.Add(new Vector3(cell.x + 1, 1, cell.y - 1));
                vertices.Add(new Vector3(cell.x + 1, 1, cell.y + 1 - 1));
                vertices.Add(new Vector3(cell.x, 1, cell.y + 1 - 1));

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

        return mesh;
    }

    Mesh GenerateCollisionMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
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
