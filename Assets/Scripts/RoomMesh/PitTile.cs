using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PitTile : Tile
{
    public override string PreviewSpriteName => Side;

    public string Side;
    public string Bottom;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, RoomMeshOptions options, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        if (Side == null || !sprites.ContainsKey(Side))
        {
            return;
        }

        var depth = sprites[Side].rect.height / (float)sprites[Side].pixelsPerUnit;

        if (Bottom != null && sprites.ContainsKey(Bottom))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, -depth, position.y), Vector2.one, Vector3.up, sprites[Bottom].uv);
        }
        
        if (neighbours.North is PitTile)
        {
            return;
        }

        tileMeshBuilder.AddTile(new Vector3(position.x, -depth, position.y + 1), new Vector2(1, depth), Vector3.back, sprites[Side].uv);
    }

    public override void AddLayerCollisionMeshes(Dictionary<string, TileMeshBuilder> meshes, Vector2Int position, TileNeighbours neighbours)
    {
        TileMeshBuilder tileMeshBuilder;
        if (!meshes.TryGetValue("Dashable", out tileMeshBuilder))
        {
            tileMeshBuilder = new TileMeshBuilder();
            meshes["Dashable"] = tileMeshBuilder;
        }

        if (!(neighbours.North is PitTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, WallTile.Height, position.y + 1), new Vector2(1, WallTile.Height), Vector3.forward);
        }

        if (!(neighbours.East is PitTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x + 1, WallTile.Height, position.y), new Vector2(WallTile.Height, 1), Vector3.right);
        }

        if (!(neighbours.South is PitTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(1, WallTile.Height), Vector3.back);
        }

        if (!(neighbours.West is PitTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(WallTile.Height, 1), Vector3.left);
        }

        TileMeshBuilder tileMeshBuilder2;
        if (!meshes.TryGetValue("Pits", out tileMeshBuilder2))
        {
            tileMeshBuilder2 = new TileMeshBuilder();
            meshes["Pits"] = tileMeshBuilder2;
        }

        tileMeshBuilder2.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up);
    }
}