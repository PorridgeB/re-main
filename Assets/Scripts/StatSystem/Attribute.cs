using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisplayType
{
    Float,
    Int,
    Percentage
}

public class Attribute : BaseAttribute
{
    private float minValue;
    private float maxValue;
    private DisplayType type;

    private List<RawBonus> rawBonuses;
    private List<FinalBonus> finalBonuses;

    private float finalValue;

    public Attribute(float value, DisplayType _type, float _minValue, float _maxValue) : base(value, 0)
    {
        finalValue = BaseValue;
        type = _type;
        minValue = _minValue;
        maxValue = _maxValue;
    }

    public void AddRawBonus(RawBonus bonus)
    {
        rawBonuses.Add(bonus);
    }

    public void RemoveRawBonus(RawBonus bonus)
    {
        rawBonuses.Remove(bonus);
    }

    public void AddFinalBonus(FinalBonus bonus)
    {
        finalBonuses.Add(bonus);
    }

    public void RemoveFinalBonus(FinalBonus bonus)
    {
        finalBonuses.Remove(bonus);
    }

    public float Value()
    {
        finalValue = BaseValue;

        float rawBonusValue = 0;
        float rawBonusMultiplier = 0;

        foreach (RawBonus b in rawBonuses)
        {
            rawBonusValue += b.BaseValue;
            rawBonusMultiplier += b.BaseMuliplier;
        }

        finalValue += rawBonusValue;
        finalValue *= rawBonusMultiplier;

        finalValue = BaseValue;

        float finalBonusValue = 0;
        float finalBonusMultiplier = 0;

        foreach (FinalBonus b in finalBonuses)
        {
            finalBonusValue += b.BaseValue;
            finalBonusMultiplier += b.BaseMuliplier;
        }

        finalValue += finalBonusValue;
        finalValue *= finalBonusMultiplier;

        return finalValue;
    }

    public string DisplayFinalValue()
    {
        float value = Value();
        switch(type)
        {
            case DisplayType.Int:
                return Mathf.Floor(value).ToString();
            case DisplayType.Percentage:
                return (value * 100) + "%";
            default:
                return value.ToString();
        }
    }
}
