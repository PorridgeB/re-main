using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PitTile : Tile
{
    public override string PreviewSpriteName => Side;

    private const float depth = 1.5f;

    public string Side;
    public string Bottom;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, RoomMeshOptions options, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        var bottomUvs = Bottom != null && sprites.ContainsKey(Bottom) ? sprites[Bottom].uv : null;
        tileMeshBuilder.AddTile(new Vector3(position.x, -depth, position.y), Vector2.one, Vector3.up, bottomUvs);

        if (neighbours.North is PitTile)
        {
            return;
        }

        var sideUvs = Side != null && sprites.ContainsKey(Side) ? sprites[Side].uv : null;
        tileMeshBuilder.AddTile(new Vector3(position.x, -depth, position.y + 1), new Vector2(1, depth), Vector3.back, sideUvs);
    }

    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
    }
}