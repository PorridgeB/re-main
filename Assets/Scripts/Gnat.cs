using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gnat : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float ExplosionDamage = 50f;
    public float MaximumExplosionDelay = 0.4f;

    public void Prime()
    {
        var behaviorTree = GetComponent<BehaviorTree>();

        behaviorTree.SetVariableValue("Primed", true);
    }

    public void Detonate(float delay = 0f)
    {
        CreateExplosion(delay);

        Destroy(gameObject);
    }

    private void CreateExplosion(float delay = 0f)
    {
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        var delayedAnimatorTrigger = explosion.GetComponent<DelayedAnimatorTrigger>();
        delayedAnimatorTrigger.Delay = delay;

        var damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Energy;
        damageInstance.source = explosion;
        damageInstance.value = ExplosionDamage;

        var damageSource = explosion.GetComponent<DamageSource>();
        damageSource.source = explosion;
        damageSource.AddInstance(damageInstance);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DamageSource"))
        {
            var source = collision.gameObject.GetComponent<DamageSource>();

            // If projectile or explosion, detonate
            var isProjectile = collision.gameObject.GetComponent<Projectile>() != null;
            var isEnergyDamage = source.Damages.Any(x => x.type == DamageType.Energy); // Explosions are energy

            if (isProjectile || isEnergyDamage)
            {
                Detonate(Random.Range(0, MaximumExplosionDelay));
            }
        }
    }

}
