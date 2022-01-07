using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectileRicochetEffect : PhaserProjectileEffect
{
    [Tooltip("Chance of the projectile ricocheting when colliding with a wall")]
    [Range(0, 1)]
    public float Chance = 0.75f;
    [Tooltip("Maximum number of times the projectile can ricochet before impacting")]
    public int MaxTimes = 4;

    private int ricochets = 0;

    private void Ricochet(Vector3 normal)
    {
        projectile.Direction = Vector3.Reflect(projectile.Direction, normal);
        projectile.Direction.y = 0;
        projectile.Direction.Normalize();

        // Move the projectile out of the wall a little
        //rigidbody.MovePosition(transform.position + Direction * 0.1f);
        transform.position += projectile.Direction * 0.2f;
    }

    // OnCollisionStayed(Collision collision)?
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            if (Random.value < Chance && ricochets++ < MaxTimes)
            {
                var normal = collision.contacts[0].normal;
                Ricochet(normal);
            }
            else
            {
                projectile.Impact();
            }
       }
    }
}
