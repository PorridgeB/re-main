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

    private void OnProjectileCollision(PhaserProjectileCollisionMessage message)
    {
        var collision = message.Collision;

        if (collision.gameObject.CompareTag(projectile.Target))
        {
            if (Random.value < Chance && passthroughs++ < MaxTimes)
            {
                projectile.CreateProjectileImpact();
                projectile.TemporarilyIgnoreCollision(collision.collider);

                message.DontImpact();
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(projectile.Target))
    //    {
    //        if (Random.value < Chance && passthroughs++ < MaxTimes)
    //        {
    //            projectile.CreateProjectileImpact();
    //            projectile.TemporarilyIgnoreCollision(collision.collider);
    //        }
    //        else
    //        {
    //            projectile.Impact(collision.collider);
    //        }
    //    }
    //}
}
