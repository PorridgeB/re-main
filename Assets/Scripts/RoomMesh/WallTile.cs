using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallTile : Tile
{
    //public override Texture2D Preview => Texture2D.blackTexture;//TextureFromSprite(Sprite);

    //public List<Sprite> WallTrim;
    //public Sprite Sprite;
    public string WallTrim;
    public string Sprite;

    private const int height = 2;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        // Wall trimmings
        foreach (var texture in ConnectedTextures.GetTextures<WallTile>(neighbours))
        {
            //var wallTrimSprite = WallTrim.Find(x => x.name == $"WallTrim_{texture}");
            var wallTrimSprite = sprites[$"{WallTrim}_{texture}"];
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

        var uv = Sprite != null ? sprites[Sprite]?.uv : null;

        if (!(neighbours.North is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, height, position.y + 1), new Vector2Int(1, height), Vector3Int.forward, null);
        }

        if (!(neighbours.East is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x + 1, height, position.y), new Vector2Int(height, 1), Vector3Int.right, null);
        }

        if (!(neighbours.South is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(1, height), Vector3Int.back, neighbours.South == null ? null : uv);
        }

        if (!(neighbours.West is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), new Vector2Int(height, 1), Vector3Int.left, null);
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

    public override Texture2D GetPreview(Sprite[] sprites)
    {
        foreach (var sprite in sprites)
        {
            if (sprite.name == Sprite)
            {
                return TextureFromSprite(sprite);
            }
        }

        return Texture2D.blackTexture;
    }
}
