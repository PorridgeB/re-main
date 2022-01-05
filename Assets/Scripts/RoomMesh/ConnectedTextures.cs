using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConnectedTextures
{
    // Each character corresponds to a compass direction: N-NE-E-SE-S-SW-W-NW
    // 1 - Tile must be of the same type or empty
    // 0 - Tile must be of a different type
    // X - Don't care
    private static readonly Dictionary<string, string> Mapping = new Dictionary<string, string>
    {
        { "EdgeN", "0X1XXX1X" },
        { "EdgeE", "1X0X1XXX" },
        { "EdgeS", "XX1X0X1X" },
        { "EdgeW", "1XXX1X0X" },
        { "InNE", "1X1X0X0X" },
        { "InSE", "0X1X1X0X" },
        { "InSW", "0X0X1X1X" },
        { "InNW", "1X0X0X1X" },
        { "ExNE", "101XXXXX" },
        { "ExSE", "XX101XXX" },
        { "ExSW", "XXXX101X" },
        { "ExNW", "1XXXXX10" },
        { "CapN", "1X0X0X0X" },
        { "CapE", "0X1X0X0X" },
        { "CapS", "0X0X1X0X" },
        { "CapW", "0X0X0X1X" },
        { "Full", "0X0X0X0X" },
    };

    private static bool Matches(string Value, string Pattern)
    {
        return Value.Zip(Pattern, (v, p) => p == 'X' || v == p).All(x => x);
    }

    public static string[] GetTextures<T>(TileNeighbours neighbours)
    {
        // Convert the neighbouring tiles to 1s and 0s
        // 1 - Neighbour is a empty or the same tile type
        // 0 - Neighbour is some other tile
        var neighbourBits = string.Join("", neighbours.Tiles.Select(x => x == null || x is T ? "1" : "0"));

        // Return the connected texture names with matching patterns
        return Mapping.Keys.Where(x => Matches(neighbourBits, Mapping[x])).ToArray();
    }
}
