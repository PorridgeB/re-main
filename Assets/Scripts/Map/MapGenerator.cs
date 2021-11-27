using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public List<Biome> Biomes;

    public Map Generate()
    {
        Map map = new Map();

        // TODO: Each biome should have its own quadrant generator?
        BasicQuadrantGenerator quadrantGen = new BasicQuadrantGenerator();

        foreach (var biome in Biomes)
        {
            map.Quadrants.Add(quadrantGen.Generate(biome));
        }

        return map;
    }
}
