using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Software : ScriptableObject
{
    public string description;
    public Sprite sprite;
    public List<Bonus> bonuses;
}
