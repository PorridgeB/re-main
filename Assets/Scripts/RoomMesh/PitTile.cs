using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PitTile : Tile
{
    public override Texture2D Preview => Texture2D.redTexture;

    private const int depth = 4;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        if (neighbours.North is PitTile)
        {
            return;
        }

        tileMeshBuilder.AddTile(new Vector3Int(position.x, -depth, position.y + 1), new Vector2Int(1, depth), Vector3Int.back, null);
    }

    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
    }
}