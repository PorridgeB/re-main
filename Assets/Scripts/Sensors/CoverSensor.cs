using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSensor : Sensor
{
    public float ExpiryTime = 16;
    public float MaxDistance = 10;

    public override void Sense()
    {
        var covers = GameObject.FindGameObjectsWithTag("Cover");

        foreach (var cover in covers)
        {
            if ((cover.transform.position - transform.position).sqrMagnitude > MaxDistance * MaxDistance)
            {
                continue;
            }

            var direction = (cover.transform.position - transform.position).normalized;

            var distance = Mathf.Min(MaxDistance, Vector3.Distance(cover.transform.position, transform.position));

            if (!Physics.Raycast(transform.position + Vector3.up, direction, out _, distance, LayerMask.GetMask("Level")))
            {
                memory.Record(new Observation(cover, ExpiryTime));
            }
        }
    }
}
