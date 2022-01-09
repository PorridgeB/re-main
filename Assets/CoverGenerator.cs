using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scans a RoomMesh and automatically generates strategic cover positions for NPCs
public class CoverGenerator : MonoBehaviour
{
    public RoomMesh RoomMesh;

    private int count = 0;

    private static bool Matches(string Value, string Pattern)
    {
        return Value.Zip(Pattern, (v, p) => p == 'X' || v == p).All(x => x);
    }

    void Start()
    {
        var patterns = new string[]
        {
            "11000000",
            "01100000",
            "00110000",
            "00011000",
            "00001100",
            "00000110",
            "00000011",
            "10000001",
            "11100000",
            "00111000",
            "00001110",
            "10000011",
        };

        foreach (var tile in RoomMesh)
        {
            if (!(tile.Tile is FloorTile))
            {
                continue;
            }

            var neighbourBits = string.Join("", tile.Neighbours.Tiles.Select(x => x is WallTile ? "1" : "0"));

            if (patterns.Where(x => Matches(neighbourBits, x)).Any())
            {
                CreateCoverPosition(tile.WorldPositionCenter);
            }
        }
    }

    private void CreateCoverPosition(Vector3 position)
    {
        var cover = new GameObject($"Cover ({++count})");

        cover.transform.parent = transform;
        cover.transform.position = position;
        cover.tag = "Cover";
        cover.AddComponent<CoverPosition>();
    }
}
