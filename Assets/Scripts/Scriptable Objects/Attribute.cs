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
    private List<Bonus> moduleBonuses = new List<Bonus>();
    [SerializeField]
    private List<Bonus> temporaryBonuses = new List<Bonus>();
    [Space]
    [SerializeField]
    private float finalValue;

    private void Awake()
    {
        moduleBonuses.Clear();
        finalValue = baseValue;
    }

    public void ClearmoduleBonuses()
    {
        moduleBonuses.Clear();
    }

    public void Reset()
    {
        moduleBonuses.Clear();
        temporaryBonuses.Clear();
    }

    public void AddModuleBonus(Bonus bonus)
    {
        moduleBonuses.Add(bonus);
    }

    public void RemoveModuleBonus(Bonus bonus)
    {
        moduleBonuses.Remove(bonus);
    }

    public void AddTemporaryBonus(Bonus bonus)
    {
        temporaryBonuses.Add(bonus);
    }

    public bool HasTemporaryBonus(Bonus bonus)
    {
        return temporaryBonuses.Contains(bonus);
    }

    public void RemoveTemporaryBonus(Bonus bonus)
    {
        temporaryBonuses.Remove(bonus);
    }

    public float Value()
    {
        finalValue = BaseValue;

        float linerValues = 0;
        float exponentialValues = 0;

        foreach (Bonus b in moduleBonuses)
        {
            linerValues += b.value;
            exponentialValues += b.multiplier;
        }

        foreach (Bonus b in temporaryBonuses)
        {
            linerValues += b.value;
            exponentialValues += b.multiplier;
        }

        finalValue += linerValues;
        finalValue *= 1 + exponentialValues;
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
