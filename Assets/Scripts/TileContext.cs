using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContext
{
    public Tile Tile;
    public Vector2Int Position;
    public TileNeighbours Neighbours;

    public Vector3 WorldPosition => new Vector3(Position.x, 0, Position.y);
    public Vector3 WorldPositionCenter => new Vector3(Position.x + 0.5f, 0, Position.y + 0.5f);
}
