using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttribute
{
    private float baseValue;
    private float baseMultiplier;

    public BaseAttribute(float value, float multiplier)
    {
        baseValue = value;
        baseMultiplier = value;
    }

    public float BaseValue => baseValue;
    public float BaseMuliplier => baseMultiplier;

    public string DisplayValue()
    {
        if (baseMultiplier < 0)
        {
            return "+" + (100 * baseMultiplier) + "%";
        }
        return "+" + baseValue;
    }
}
