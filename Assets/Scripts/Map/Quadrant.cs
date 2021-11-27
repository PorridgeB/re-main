using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrant
{
    // The first index is the layer.
    // The second index is the stage within that layer.
    // Layer 0 is the 1st layer.
    // A quadrant must start and end with one stage.
    public Stage[][] Stages;

    public Biome Biome;

    // The path the player has taken.
    // A list of stage indices, starting from layer 0.
    //public List<int> Path;

    //public Stage CurrentStage => Stages[Path.Count - 1][Path[Path.Count - 1]];

    //public void Enter()
    //{
    //    Path = new List<int>() { 0 };
    //}

    //public void Step(int index)
    //{
    //    Path.Add(CurrentStage.Next[index]);
    //}
}
