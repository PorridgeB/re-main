using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Common")]
public class GoTo : Action
{
    public SharedVector3 TargetPosition;
    public SharedFloat Speed;
    public float TargetReachedRadius = 0.5f;

    public override TaskStatus OnUpdate()
    {
        var agent = GetComponent<NavMeshAgent>();

        agent.speed = Speed.Value;
        agent.destination = TargetPosition.Value;

        if (Vector3.Distance(transform.position, TargetPosition.Value) < TargetReachedRadius)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
