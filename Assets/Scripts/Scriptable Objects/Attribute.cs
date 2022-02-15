using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private List<Bonus> softwareBonuses = new List<Bonus>();
    [SerializeField]
    private List<Bonus> moduleBonuses = new List<Bonus>();
    [SerializeField]
    private List<Bonus> temporaryBonuses = new List<Bonus>();
    [Space]
    [SerializeField]
    private float finalValue;

    private void Awake()
    {
        finalValue = baseValue;
    }

    public void Reset()
    {
        softwareBonuses.Clear();
        moduleBonuses.Clear();
        temporaryBonuses.Clear();
    }

    public void AddSoftwareBonus(Bonus bonus)
    {
        softwareBonuses.Add(bonus);
    }

    public void RemoveSoftwareBonus(Bonus bonus)
    {
        softwareBonuses.Remove(bonus);
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

        

        AddBonusValues(softwareBonuses, ref finalValue);
        AddBonusValues(moduleBonuses, ref finalValue);
        AddBonusValues(temporaryBonuses, ref finalValue);

        return Mathf.Clamp(finalValue, minValue, maxValue);
    }

    public void AddBonusValues(List<Bonus> bonuses, ref float finalValue)
    {
        float linearValues = 0;
        float exponentialValues = 1;

        foreach (Bonus b in bonuses)
        {
            linearValues += b.value;
            exponentialValues += b.multiplier;
        }
        finalValue += linearValues;
        finalValue *= exponentialValues;
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
