using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public enum StageState
    {
        // The player is currently within this stage.
        Here,

        // The player has already played through this stage.
        Visited,

        // The player is able to reach this stage.
        Reachable,

        // The player is no longer able to reach this stage.
        Unreachable
    }

    // TODO: Use biome from Quadrant.
    public Biome Biome;

    public StageType Type;

    public StageState State = StageState.Reachable;

    // The list of indicies into the next layer that this stage connects to.
    public List<int> Next;
}
