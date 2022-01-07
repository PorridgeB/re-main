using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectilePassthroughEffect : PhaserProjectileEffect
{
    [Tooltip("Chance of the projectile passing through a target when colliding with one")]
    [Range(0, 1)]
    public float Chance = 1;
    [Tooltip("Maximum number of target collisions before the projectile is destroyed")]
    public int MaxTimes = 1;

    private int passthroughs = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(projectile.Target))
        {
            if (Random.value < Chance && passthroughs++ < MaxTimes)
            {
                projectile.CreateImpactEffect();
                projectile.IgnoreCollision(collision.collider);
            }
        }
    }
}
