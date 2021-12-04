using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Energy,
    Elemental
}

public class DamageSource : MonoBehaviour
{
    private DamageInstance damageInstance;

    public DamageInstance Damage
    {
        get
        {
            return damageInstance;
        }
    }

    public void SetValue(DamageInstance instance)
    {
        damageInstance = instance;
    }
}
