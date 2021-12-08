using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class RandomPosition : Action
{
    public SharedVector3 Position;
    public float Radius = 10f;

    public override void OnStart()
    {
        var offset = Random.insideUnitCircle * Radius;
        var randomPosition = transform.position + new Vector3(offset.x, 0, offset.y);

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, Radius, 1);

        Position.Value = hit.position;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
