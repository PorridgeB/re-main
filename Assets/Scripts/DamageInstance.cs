using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageInstance
{
    public GameObject source;
    public float value;
    public bool crit;
    public DamageType type;
}
