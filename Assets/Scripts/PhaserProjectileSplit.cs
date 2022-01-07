using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectileSplit : PhaserProjectileEffect
{
    [Tooltip("Chance of the projectile splitting after impacting")]
    [Range(0, 1)]
    public float Chance = 1;
    [Tooltip("Minimum number of new projectiles")]
    public int MinProjectiles = 2;
    [Tooltip("Maximum number of new projectiles")]
    public int MaxProjectiles = 3;
    [Tooltip("Arc angle that indicates the spread of the new projectiles")]
    [Range(0, 180)]
    public float ArcAngle = 30;
    [Tooltip("Size factor of the new projectiles")]
    public float SizeMultiplier = 0.7f;

    private void Split(Collider collider)
    {
        var projectiles = Random.Range(MinProjectiles, MaxProjectiles);

        for (int i = 0; i < projectiles; i++)
        {
            var newProjectile = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<PhaserProjectile>();

            newProjectile.IgnoreCollision(collider);

            var angle = i * (ArcAngle / (projectiles - 1)) - ArcAngle / 2;
            newProjectile.Direction = Quaternion.Euler(0, angle, 0) * projectile.Direction;

            newProjectile.Size = projectile.Size * SizeMultiplier;

            // Really important! Or else the projectiles will grow exponentially
            Destroy(newProjectile.GetComponent<PhaserProjectileSplit>());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(projectile.Target))
        {
            if (Random.value < Chance)
            {
                Split(collision.collider);
            }

            projectile.Impact();
        }
    }
}
