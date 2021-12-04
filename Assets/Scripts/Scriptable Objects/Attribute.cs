using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisplayType
{
    Float,
    Int,
    Percentage
}
[CreateAssetMenu]
public class Attribute : BaseAttribute
{
    [SerializeField]
    private float minValue;
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private DisplayType type;
    [SerializeField]
    private List<RawBonus> rawBonuses = new List<RawBonus>();
    [SerializeField]
    private List<Bonus> moduleBonuses = new List<Bonus>();
    [Space]
    [SerializeField]
    private float finalValue;

    private void Awake()
    {
        rawBonuses.Clear();
        moduleBonuses.Clear();
        finalValue = baseValue;
    }

    public void AddRawBonus(RawBonus bonus)
    {
        rawBonuses.Add(bonus);
        
    }

    public void RemoveRawBonus(RawBonus bonus)
    {
        rawBonuses.Remove(bonus);
    }

    public void ClearmoduleBonuses()
    {
        moduleBonuses.Clear();
    }

    public void AddModuleBonus(Bonus bonus)
    {
        moduleBonuses.Add(bonus);
    }

    public void RemoveModuleBonus(Bonus bonus)
    {
        moduleBonuses.Remove(bonus);
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
        finalValue *= 1 + rawBonusMultiplier;

        finalValue = BaseValue;

        float moduleBonusValue = 0;
        float moduleBonusMultiplier = 0;

        foreach (Bonus b in moduleBonuses)
        {
            moduleBonusValue += b.value;
            moduleBonusMultiplier += b.multiplier;
        }

        finalValue += moduleBonusValue;
        finalValue *= 1 + moduleBonusMultiplier;
        return Mathf.Clamp(finalValue, minValue, maxValue);
    }

    public string DisplayFinalValue()
    {
        Value();
        switch(type)
        {
            case DisplayType.Int:
                return Mathf.Floor(finalValue).ToString();
            case DisplayType.Percentage:
                return (finalValue * 100) + "%";
            default:
                return finalValue.ToString();
        }
    }
}
