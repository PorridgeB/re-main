using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloorTile : Tile
{
    public override string PreviewSpriteName => Sprite;

    public string Sprite;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, RoomMeshOptions options, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        var colors = new Color[]
        {
            (neighbours.North is WallTile || neighbours.West is WallTile || neighbours.NorthWest is WallTile) ? options.AmbientOcclusionColor : Color.white,
            (neighbours.North is WallTile || neighbours.East is WallTile || neighbours.NorthEast is WallTile) ? options.AmbientOcclusionColor : Color.white,
            (neighbours.South is WallTile || neighbours.West is WallTile || neighbours.SouthWest is WallTile) ? options.AmbientOcclusionColor : Color.white,
            (neighbours.South is WallTile || neighbours.East is WallTile || neighbours.SouthEast is WallTile) ? options.AmbientOcclusionColor : Color.white,
        };

        tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up, sprites[Sprite]?.uv, colors);
    }
    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up);
    }
}
