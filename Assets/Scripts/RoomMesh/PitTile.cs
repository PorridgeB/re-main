using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PitTile : Tile
{
    public const float Depth = 1.5f;

    public override string PreviewSpriteName => Side;

    public string Side;
    public string Bottom;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, RoomMeshOptions options, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        var bottomUvs = Bottom != null && sprites.ContainsKey(Bottom) ? sprites[Bottom].uv : null;
        tileMeshBuilder.AddTile(new Vector3(position.x, -Depth, position.y), Vector2.one, Vector3.up, bottomUvs);

        if (neighbours.North is PitTile)
        {
            return;
        }

        var sideUvs = Side != null && sprites.ContainsKey(Side) ? sprites[Side].uv : null;
        tileMeshBuilder.AddTile(new Vector3(position.x, -Depth, position.y + 1), new Vector2(1, Depth), Vector3.back, sideUvs);
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
    }
}