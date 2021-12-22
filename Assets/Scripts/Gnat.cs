using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gnat : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float ExplosionDamage = 50f;

    public void Prime()
    {
        var behaviorTree = GetComponent<BehaviorTree>();

        behaviorTree.SetVariableValue("Primed", true);
    }

    public void Detonate()
    {
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        DamageInstance damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Energy;
        //damageInstance.source = gameObject;
        damageInstance.source = explosion;
        damageInstance.value = ExplosionDamage;

        var damageSource = explosion.GetComponent<DamageSource>();

        //damageSource.source = gameObject;
        damageSource.source = explosion;
        damageSource.AddInstance(damageInstance);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DamageSource"))
        {
            DamageSource source = collision.gameObject.GetComponent<DamageSource>();

            // If projectile or explosion, detonate
            var isProjectile = collision.gameObject.GetComponent<Projectile>() != null;
            var isEnergyDamage = source.Damages.Any(x => x.type == DamageType.Energy); // Explosions are energy

            if (isProjectile || isEnergyDamage)
            {
                Detonate();
            }
        }
    }

}
