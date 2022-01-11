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

                if (!Physics.Raycast(transform.position + Vector3.up, direction, out _, MaxDistance, LayerMask.GetMask("Level")))
                {
                    memory.Record(new Observation(other, ExpiryTime));
                }
            }
        }
    }
}
