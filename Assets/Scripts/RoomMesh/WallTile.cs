using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallTile : Tile
{
    public override string PreviewSpriteName => Sprite;

    public string Trim;
    public string Sprite;

    private const float height = 1.5f;

    public override void AddMesh(TileMeshBuilder tileMeshBuilder, RoomMeshOptions options, Dictionary<string, Sprite> sprites, Vector2Int position, TileNeighbours neighbours)
    {
        // Wall trimmings
        foreach (var texture in ConnectedTextures.GetTextures<WallTile>(neighbours))
        {
            //var wallTrimSprite = WallTrim.Find(x => x.name == $"WallTrim_{texture}");
            var wallTrimSprite = sprites[$"{Trim}_{texture}"];
            if (wallTrimSprite)
            {
                tileMeshBuilder.AddTile(new Vector3(position.x, height + 0.01f, position.y), Vector2Int.one, Vector3.up, wallTrimSprite.uv);
            }
        }

        // Wall trimmings background
        tileMeshBuilder.AddTile(new Vector3(position.x, height, position.y), Vector2Int.one, Vector3.up, null);

        // Front facing wall
        //if (neighbours.South is WallTile)
        //{
        //    return;
        //}

        var uv = Sprite != null ? sprites[Sprite]?.uv : null;

        if (options.AddHiddenWallFaces)
        {
            if (!(neighbours.North is WallTile))
            {
                tileMeshBuilder.AddTile(new Vector3(position.x, height, position.y + 1), new Vector2(1, height), Vector3.forward, null);
            }

            if (!(neighbours.East is WallTile))
            {
                tileMeshBuilder.AddTile(new Vector3(position.x + 1, height, position.y), new Vector2(height, 1), Vector3.right, null);
            }

            if (!(neighbours.West is WallTile))
            {
                tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(height, 1), Vector3.left, null);
            }
        }

        if (!(neighbours.South is WallTile))
        {
            Color[] colors = null;
            if (options.BakeAmbientOcclusion)
            {
                colors = new Color[]
                {
                    options.CalculateAmbientOcclusion(false, neighbours.SouthWest is WallTile),
                    options.CalculateAmbientOcclusion(false, neighbours.SouthEast is WallTile),
                    options.CalculateAmbientOcclusion(true, neighbours.SouthWest is WallTile),
                    options.CalculateAmbientOcclusion(true, neighbours.SouthEast is WallTile),
                };
            }

            tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(1, height), Vector3.back, neighbours.South == null ? null : uv, colors);
        }

        //tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(1, height), Vector3.back, neighbours.South == null ? null : uv);
    }

    public override void AddCollisionMesh(TileMeshBuilder tileMeshBuilder, Vector2Int position, TileNeighbours neighbours)
    {
        if (!(neighbours.North == null || neighbours.North is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, height, position.y + 1), new Vector2(1, height), Vector3.forward);
        }

        if (!(neighbours.East == null || neighbours.East is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x + 1, height, position.y), new Vector2(height, 1), Vector3.right);
        }

        if (!(neighbours.South == null || neighbours.South is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(1, height), Vector3.back);
        }

        if (!(neighbours.West == null || neighbours.West is WallTile))
        {
            tileMeshBuilder.AddTile(new Vector3(position.x, 0, position.y), new Vector2(height, 1), Vector3.left);
        }
    }
}
