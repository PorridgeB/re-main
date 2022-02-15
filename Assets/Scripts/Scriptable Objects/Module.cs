using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public class Bonus
{
    public Attribute attribute;
    public string source;
    public float value;
    public float multiplier;
}

[System.Serializable]
[CreateAssetMenu]
public class Module : ScriptableObject
{
    public void Awake()
    {
        count = 0;
        foreach (Bonus b in bonuses)
        {
            b.source = name;
        }
    }

    public int count;
    [Space]
    public string description;
    public Rarity rarity;
    public Sprite sprite;
    public List<Bonus> bonuses;
}
