using System;
using System.Linq;
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
        Color[] colors = null;
        if (options.BakeAmbientOcclusion)
        {
            colors = new Color[]
            {
                options.CalculateAmbientOcclusion(neighbours.North is WallTile, neighbours.West is WallTile, neighbours.NorthWest is WallTile),
                options.CalculateAmbientOcclusion(neighbours.North is WallTile, neighbours.East is WallTile, neighbours.NorthEast is WallTile),
                options.CalculateAmbientOcclusion(neighbours.South is WallTile, neighbours.West is WallTile, neighbours.SouthWest is WallTile),
                options.CalculateAmbientOcclusion(neighbours.South is WallTile, neighbours.East is WallTile, neighbours.SouthEast is WallTile),
            };
        }

        tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up, sprites[Sprite]?.uv, colors);

        //if (neighbours.South == null)
        //{
        //    tileMeshBuilder.AddTile(new Vector3(position.x, -1, position.y), new Vector2(1, 1), Vector3.back, sprites[Sprite]?.uv);
        //}
    }

    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up);
    }
}
