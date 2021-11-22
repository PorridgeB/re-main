using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome
{
    public static readonly Biome[] Biomes =
    {
        new Biome{ Name = "Wreakage" },
        new Biome{ Name = "Gardens" },
        new Biome{ Name = "Stasis" },
        new Biome{ Name = "First Class" },
    };

    public string Name;

    public string Description;

    public Color BackgroundColor;

    // TODO: Modifiers
    // TODO: Each biome has its own quadrant and stage generator?
}
