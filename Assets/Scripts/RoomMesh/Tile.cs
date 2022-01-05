using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Tile
{
    //public virtual Texture2D Preview { get; }

    public static Texture2D TextureFromSprite(Sprite sprite)
    {
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);

        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }

    public virtual void AddMesh(TileMeshBuilder tileMeshBuilder, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
    }

    public virtual void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
    }

    public virtual Texture2D GetPreview(Sprite[] sprites)
    {
        return Texture2D.blackTexture;
    }
}
