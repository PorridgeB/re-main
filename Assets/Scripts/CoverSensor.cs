using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSensor : Sensor
{
    public float MaxDistance = 10;

    public override void Sense()
    {
        var covers = GameObject.FindGameObjectsWithTag("Cover");

        foreach (var cover in covers)
        {
            if ((cover.transform.position - transform.position).magnitude > MaxDistance)
            {
                continue;
            }

            var direction = (cover.transform.position - (transform.position + Vector3.up)).normalized;

            if (Physics.Raycast(transform.position + Vector3.up, direction, out RaycastHit hit, MaxDistance, LayerMask.GetMask("Level")))
            {
                if ((hit.point - cover.transform.position).magnitude < 1)
                {
                    memory.Record(new Observation(cover.transform.position, cover));
                }
            }
        }
    }
}
