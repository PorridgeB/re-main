using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public List<Quadrant> Quadrants = new List<Quadrant>();

    public List<int> Path = new List<int>() { 0 };

    public Stage CurrentStage()
    {
        int currentLayer = Path.Count;
        Quadrant currentQuadrant = null;

        for (int i = 0; i < Quadrants.Count; i++)
        {
            var quadrant = Quadrants[i];
            var quadrantLayers = quadrant.Stages.Length;

            if (quadrantLayers > currentLayer)
            {
                currentQuadrant = quadrant;
                break;
            }

            currentLayer -= quadrantLayers;
        }

        return currentQuadrant.Stages[currentLayer][Path[Path.Count - 1]];
    }

    public void Step(int index)
    {

    }
}
