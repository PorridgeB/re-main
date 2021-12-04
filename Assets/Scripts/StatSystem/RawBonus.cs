using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RawBonus : BaseAttribute
{
    RawBonus(float value, float multiplier)
    {
        baseValue = value;
        baseMultiplier = multiplier;
    }
}
