using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FinalBonus : BaseAttribute
{
    public Module source;
    public FinalBonus(Module module, float value, float multiplier)
    {
        source = module;
        baseValue = value;
        baseMultiplier = multiplier;
    }
}
