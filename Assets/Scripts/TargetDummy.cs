using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    [SerializeField]
    private float damageTotal;
    [SerializeField]
    private GameObject damageTokenPrefab;
    [SerializeField]
    private Transform damageTokenSpawn;

    public void OnDamage(DamageSource source)
    {
        foreach (var instance in source.Damages)
        {
            damageTotal += instance.value;
            var damageToken = Instantiate(damageTokenPrefab, damageTokenSpawn.position, Quaternion.identity);
            damageToken.GetComponent<DamageToken>().SetValue(instance);
        }
    }
}
