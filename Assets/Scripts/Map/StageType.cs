using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StageType", menuName = "StageType")]
public class StageType : ScriptableObject
{
    public string Name;

    public string Description;

    public Sprite Icon;

    // A measure of how desirable this stage type is for the player to encounter.
    // Positive values are considered desirable, while negative values are consideried undesirable.
    public float Utility = 1;

    public float Weight = 1;

    // TODO: StageModifiers
}
