using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNeighbours
{
    public static readonly Vector2Int[] Offsets = new Vector2Int[8]
    {
        new Vector2Int(0, 1), // N
        new Vector2Int(1, 1), // NE
        new Vector2Int(1, 0), // E
        new Vector2Int(1, -1), // SE
        new Vector2Int(0, -1), // S
        new Vector2Int(-1, -1), // SW
        new Vector2Int(-1, 0), // W
        new Vector2Int(-1, 1), // NW
    };

    public Tile[] Tiles;

    public Tile North => Tiles[(int)Direction.North];
    public Tile NorthEast => Tiles[(int)Direction.NorthEast];
    public Tile East => Tiles[(int)Direction.East];
    public Tile SouthEast => Tiles[(int)Direction.SouthEast];
    public Tile South => Tiles[(int)Direction.South];
    public Tile SouthWest => Tiles[(int)Direction.SouthWest];
    public Tile West => Tiles[(int)Direction.West];
    public Tile NorthWest => Tiles[(int)Direction.NorthWest];

    public TileNeighbours(Tile[] tiles)
    {
        Tiles = tiles;
    }
}
