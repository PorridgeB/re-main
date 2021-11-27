using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "Biome")]
public class Biome : ScriptableObject
{
    public string Name;
    public string Description;
    public Color BackgroundColor;
    public int MinLayers = 5;
    public int MaxLayers = 7;
    public int MinStagesPerLayer = 1;
    public int MaxStagesPerLayer = 4;
    public List<StageType> StageTypes;

    // TODO: Modifiers
    // TODO: Each biome has its own quadrant and stage generator?
}
