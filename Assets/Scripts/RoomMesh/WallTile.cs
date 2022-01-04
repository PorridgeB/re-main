using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallTile : Tile
{
    public override Texture2D Preview => TextureFromSprite(Sprite);

    public List<Sprite> WallTrim;
    public Sprite Sprite;

    private const int height = 2;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        // Wall trimmings
        foreach (var texture in ConnectedTextures.GetTextures<WallTile>(neighbours))
        {
            var wallTrimSprite = WallTrim.Find(x => x.name == $"WallTrim_{texture}");
            if (wallTrimSprite)
            {
                tileMeshBuilder.AddTile(new Vector3(position.x, height + 0.01f, position.y), Vector2Int.one, Vector3Int.up, wallTrimSprite.uv);
            }
        }

        // Wall trimmings background
        tileMeshBuilder.AddTile(new Vector3Int(position.x, height, position.y), Vector2Int.one, Vector3Int.up, null);

        // Front facing wall
        //if (neighbours.South is WallTile)
        //{
        //    return;
        //}

        var uv = Sprite != null ? Sprite.uv : null;

        if (!(neighbours.North is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, height, position.y + 1), new Vector2Int(1, height), Vector3Int.forward, uv);
        }

        if (!(neighbours.East is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x + 1, height, position.y), new Vector2Int(height, 1), Vector3Int.right, uv);
        }

        if (!(neighbours.South is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(1, height), Vector3Int.back, neighbours.South == null ? null : uv);
        }

        if (!(neighbours.West is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(height, 1), Vector3Int.left, uv);
        }

        //tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(1, height), Vector3Int.back, neighbours.South == null ? null : uv);
    }

    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        if (!(neighbours.North == null || neighbours.North is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, height, position.y + 1), new Vector2Int(1, height), Vector3Int.forward);
        }

        if (!(neighbours.East == null || neighbours.East is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x + 1, height, position.y), new Vector2Int(height, 1), Vector3Int.right);
        }

        if (!(neighbours.South == null || neighbours.South is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(1, height), Vector3Int.back);
        }

        if (!(neighbours.West == null || neighbours.West is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(height, 1), Vector3Int.left);
        }
    }
}
