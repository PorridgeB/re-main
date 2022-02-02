using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponAttachment : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;
    public Sprite Icon;
    public int Cost;
    public string Weapon;
}
