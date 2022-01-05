using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloorTile : Tile
{
    //public override Texture2D Preview => Texture2D.blackTexture;//TextureFromSprite(Sprite);

    //public Sprite Sprite;
    public string Sprite;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up, sprites[Sprite]?.uv);
    }
    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        tileMeshBuilder.AddTile(new Vector3Int(position.x, 0, position.y), Vector2Int.one, Vector3Int.up);
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
