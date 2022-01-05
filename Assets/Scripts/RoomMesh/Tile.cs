using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Tile
{
    public virtual string PreviewSpriteName => "";

    public virtual void AddMesh(TileMeshBuilder tileMeshBuilder, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
    }

    public virtual void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
    }
}
