using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttribute : ScriptableObject
{
    [SerializeField]
    protected float baseValue;
    [SerializeField]
    protected float baseMultiplier;

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
