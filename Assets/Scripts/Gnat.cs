using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float ExplosionDamage = 50f;

    public void Detonate()
    {
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        DamageInstance damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Physical;
        //damageInstance.source = gameObjectk;
        damageInstance.source = explosion;
        damageInstance.value = ExplosionDamage;

        var damageSource = explosion.GetComponent<DamageSource>();

        damageSource.AddInstance(damageInstance);
        //damageSource.source = gameObject;
        damageSource.source = explosion;

        Destroy(gameObject);
    }
}
