using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Bonus
{
    [HideInInspector]
    public Attribute attribute;
    public Module module;
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
    }

    public int count;
    [Space]
    public string description;
    public Sprite sprite;
    public List<Bonus> bonuses;
}
