using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SightSensor : Sensor
{
    public float ExpiryTime = 5;
    public float MaxDistance = 7;
    public List<string> Tags = new List<string> { "Player", "Enemy", "Projectile" };

    public override void Sense()
    {
        var colliders = Physics.OverlapSphere(transform.position, MaxDistance);

        foreach (var collider in colliders)
        {
            var other = collider.gameObject;

            if (other == gameObject)
            {
                continue;
            }

            if (Tags.Exists(tag => other.CompareTag(tag)))
            {
                var direction = (other.transform.position - transform.position).normalized;
                direction.y = 0;
                direction.Normalize();

                var distance = Mathf.Min(MaxDistance, Vector3.Distance(other.transform.position, transform.position));

                if (!Physics.Raycast(transform.position + Vector3.up, direction, out _, distance, LayerMask.GetMask("Level")))
                {
                    memory.Record(new Observation(other, ExpiryTime));

                    // If the object is a projectile, then observe the source of who fired that projectile
                    // Kinda hacky way to start pursing a target that has fired an incoming projectile
                    if (other.CompareTag("Projectile"))
                    {
                        var projectile = other.GetComponent<PhaserProjectile>();
                        memory.Record(new Observation(projectile.Source, ExpiryTime));
                    }
                }
            }
        }
    }
}
