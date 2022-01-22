using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;

[Serializable]
[ExecuteAlways]
public class RoomMesh : MonoBehaviour, IEnumerable<TileContext>
{
    public const string DefaultMaterialPath = "RoomMaterial";
    public const string DefaultTilesetPath = "Tileset";

    [Serializable]
    public class TileInstance
    {
        public Vector2Int Position;
        [SerializeReference]
        public Tile Tile;
    }

    public RoomMeshOptions Options;

    [SerializeField]
    private List<TileInstance> Tiles = new List<TileInstance>();

    public void Clear()
    {
        Tiles.Clear();
    }

    public void PlaceTile(Vector2Int position, Tile tile)
    {
        var placedTile = Tiles.Find(x => x.Position == position);
        if (placedTile != null)
        {
            placedTile.Tile = tile;
        }
        else
        {
            Tiles.Add(new TileInstance { Position = position, Tile = tile });
        }
    }

    public bool RemoveTile(Vector2Int position)
    {
        return Tiles.RemoveAll(x => x.Position == position) > 0;
    }

    public void FillOutline(Tile tile)
    {
        var outlinePositions = new HashSet<Vector2Int>();

        foreach (var instance in Tiles)
        {
            var position = instance.Position;
            var neighbours = GetTileNeighbours(position);

            for (int i = 0; i < 8; i++)
            {
                if (neighbours.Tiles[i] == null)
                {
                    outlinePositions.Add(position + TileNeighbours.Offsets[i]);
                }
            }
        }

        foreach (var outlinePosition in outlinePositions)
        {
            PlaceTile(outlinePosition, tile);
        }
    }

    public void FillRect(RectInt rect, Tile tile)
    {
        foreach (var position in rect.allPositionsWithin)
        {
            PlaceTile(position, tile);
        }
    }

    public void RemoveRect(RectInt rect)
    {
        foreach (var position in rect.allPositionsWithin)
        {
            RemoveTile(position);
        }
    }

    public void MoveToOrigin()
    {
        var rect = GetRect();
    
        foreach (var instance in Tiles)
        {
            instance.Position -= new Vector2Int(rect.xMin, rect.yMin);
        }
    }

    public void Rotate()
    {
        var rect = GetRect();

        foreach (var instance in Tiles)
        {
            instance.Position = new Vector2Int(-instance.Position.y, instance.Position.x);
        }

        MoveToOrigin();
    }

    public void FlipX()
    {
        var rect = GetRect();

        foreach (var instance in Tiles)
        {
            instance.Position = new Vector2Int(rect.width - instance.Position.x, instance.Position.y);
        }
    }

    public void FlipY()
    {
        var rect = GetRect();

        foreach (var instance in Tiles)
        {
            instance.Position = new Vector2Int(instance.Position.x, rect.height - instance.Position.y);
        }
    }

    public void Rebuild()
    {
        if (Options == null)
        {
            Debug.LogError("RoomMeshOptions not set!");
            return;
        }

        var tileset = Resources.LoadAll<Sprite>(DefaultTilesetPath);
        var sprites = new Dictionary<string, Sprite>();
       
        foreach (var sprite in tileset)
        {
            sprites[sprite.name] = sprite;
        }

        var meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = Resources.Load<Material>(DefaultMaterialPath);
        }

        Tiles.RemoveAll(x => x.Tile == null);

        var mesh = CreateMesh(sprites);
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshFilter.sharedMesh = mesh;

        var collisionMesh = CreateCollisionMesh();
        var meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        meshCollider.sharedMesh = collisionMesh;

        CreateLayerCollisionMeshes();
    }

    public RectInt GetRect()
    {
        var rect = new RectInt();

        if (Tiles.Count == 0)
        {
            return rect;
        }

        var xMin = int.MaxValue;
        var xMax = int.MinValue;
        var yMin = int.MaxValue;
        var yMax = int.MinValue;

        foreach (var instance in Tiles)
        {
            xMin = Mathf.Min(xMin, instance.Position.x);
            xMax = Mathf.Max(xMax, instance.Position.x);
            yMin = Mathf.Min(yMin, instance.Position.y);
            yMax = Mathf.Max(yMax, instance.Position.y);
        }

        rect.SetMinMax(new Vector2Int(xMin, yMin), new Vector2Int(xMax, yMax));

        return rect;
    }

    private void Start()
    {
        Rebuild();
    }

    private Mesh CreateMesh(Dictionary<string, Sprite> sprites)
    {
        var tileMeshBuilder = new TileMeshBuilder();

        foreach (var instance in Tiles)
        {
            var position = instance.Position;
            var tile = instance.Tile;
            var neighbours = GetTileNeighbours(position);
            tile.AddMesh(tileMeshBuilder, Options, sprites, position, neighbours);
        }

        return tileMeshBuilder.ToMesh();
    }

    private Mesh CreateCollisionMesh()
    {
        var tileMeshBuilder = new TileMeshBuilder();

        foreach (var instance in Tiles)
        {
            var position = instance.Position;
            var tile = instance.Tile;
            var neighbours = GetTileNeighbours(position);
            tile.AddCollisionMesh(tileMeshBuilder, position, neighbours);
        }

        return tileMeshBuilder.ToMesh();
    }

    private void CreateLayerCollisionMeshes()
    {
        var meshBuilders = new Dictionary<string, TileMeshBuilder>();

        // Generate the layer mesh colliders
        foreach (var instance in Tiles)
        {
            var position = instance.Position;
            var tile = instance.Tile;
            var neighbours = GetTileNeighbours(position);
            tile.AddLayerCollisionMeshes(meshBuilders, position, neighbours);
        }

        // Create a LayerColliders game object to store the individual layer mesh colliders if there isn't one already
        var layerCollidersObject = transform.Find("LayerColliders")?.gameObject;
        if (layerCollidersObject == null)
        {
            layerCollidersObject = new GameObject("LayerColliders");
            layerCollidersObject.transform.parent = transform;
        }

        // Destroy the old layer collider game objects
        foreach (Transform child in layerCollidersObject.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        foreach (var pair in meshBuilders)
        {
            var layerName = pair.Key;
            var meshBuilder = pair.Value;

            var layerColliderObject = new GameObject(layerName);
            layerColliderObject.transform.parent = layerCollidersObject.transform;
            layerColliderObject.layer = LayerMask.NameToLayer(layerName);
            layerColliderObject.transform.localPosition = Vector3.zero;

            var layerMeshCollider = layerColliderObject.AddComponent<MeshCollider>();
            layerMeshCollider.sharedMesh = meshBuilder.ToMesh();
        }
    }

    private TileNeighbours GetTileNeighbours(Vector2Int position)
    {
        var neighbourTiles = new Tile[8];

        for (int i = 0; i < TileNeighbours.Offsets.Length; i++)
        {
            var neighbourPosition = position + TileNeighbours.Offsets[i];
            var neighbourInstance = Tiles.Find(x => x.Position == neighbourPosition);
            neighbourTiles[i] = neighbourInstance?.Tile;
        }

        return new TileNeighbours { Tiles = neighbourTiles };
    }

    public IEnumerator<TileContext> GetEnumerator()
    {
        foreach (var instance in Tiles)
        {
            yield return new TileContext { Tile = instance.Tile, Position = instance.Position, Neighbours = GetTileNeighbours(instance.Position) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
