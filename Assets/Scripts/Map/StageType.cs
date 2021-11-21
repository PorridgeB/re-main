using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageType
{
    public static readonly StageType[] StageTypes =
    {
        new StageType{ Name = "Normal" },
        new StageType{ Name = "Reward" },
        new StageType{ Name = "Danger" },
        new StageType{ Name = "Boss" },
        new StageType{ Name = "Mechanic" },
        new StageType{ Name = "Gardeners" },
    };

    public string Name;

    public string Description;

    public Sprite Icon;

    // A measure of how desirable this stage type is for the player to encounter.
    // Positive values are considered desirable, while negative values are consideried undesirable.
    public float Utility = 1;

    // Describes how frequent the stage type is chosen.
    // A higher weight value means it'll be chosen more frequently.
    public float Weight = 1;

    // TODO: StageModifiers
}
