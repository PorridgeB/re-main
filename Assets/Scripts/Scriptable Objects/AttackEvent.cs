using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackEvent : ScriptableObject
{
    public List<DamageInstance> damageInstances = new List<DamageInstance>();
}
