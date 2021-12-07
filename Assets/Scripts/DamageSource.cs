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
    private List<DamageInstance> damageInstances = new List<DamageInstance>();

    public List<DamageInstance> Damages
    {
        get
        {
            return damageInstances;
        }
    }

    public void AddInstance(DamageInstance instance)
    {
        damageInstances.Add(instance);
    }
}
