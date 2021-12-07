using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Attack : ScriptableObject
{
    public List<DamageInstance> damageInstances = new List<DamageInstance>();
}
