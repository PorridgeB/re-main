using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensor : Sensor
{
    public float MaxDistance = 7;

    public override void Sense()
    {
        var mask = new LayerMask { value = LayerMask.GetMask("Player") | LayerMask.GetMask("Level") };
        var target = PlayerController.instance.gameObject;
        var to = target.transform.position + Vector3.up;
        var from = transform.position + Vector3.up;
        var direction = (to - from).normalized;
        if (Physics.Raycast(from, direction, out RaycastHit raycastHit, MaxDistance, mask))
        {
            //if ((raycastHit.point - target.transform.position).magnitude < 2f)
            if (raycastHit.collider.gameObject == target)
            {
                Debug.Log("hey");
                memory.Record(new Observation(target.transform.position, target));
            }
        }
    }
}
