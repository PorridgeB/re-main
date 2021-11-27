using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicQuadrantGenerator : QuadrantGenerator
{
    public Quadrant Generate(Biome biome)
    {
        var quadrant = new Quadrant();

        quadrant.Biome = biome;

        int layers = Random.Range(biome.MinLayers, biome.MaxLayers);

        quadrant.Stages = new Stage[layers][];

        // For each layer
        for (int i = 0; i < layers; i++)
        {
            var firstOrLastLayer = i == 0 || i == layers - 1;

            int stages = firstOrLastLayer ? 1 : Random.Range(biome.MinStagesPerLayer, biome.MaxStagesPerLayer);

            quadrant.Stages[i] = new Stage[stages];

            for (int j = 0; j < stages; j++)
            {
                var stageType = biome.StageTypes[Random.Range(0, biome.StageTypes.Count - 1)];

                quadrant.Stages[i][j] = new Stage { Biome = biome, Type = stageType, Next = new int[] { 0, 1 } };
            }
        }

        return quadrant;
    }
}
