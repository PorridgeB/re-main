using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Bonus
{
    public string attributeName;
    public float value;
    public float multiplier;
    
}

[System.Serializable]
public class Module
{
    public string name;
    public string description;
    public Sprite sprite;
    public List<Bonus> bonuses;

    public Module(string _name, string _description, List<Bonus> _bonuses)
    {
        name = _name;
        description = _description;
        bonuses = _bonuses;
    }
}
