using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Common")]
public class WithinSight : Conditional
{
    public SharedFloat SightDistance;
    public SharedGameObject Target;

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            return TaskStatus.Failure;
        }

        var direction = (Target.Value.transform.position - transform.position).normalized;
        if (!Physics.Raycast(transform.position + Vector3.up, direction, out _, SightDistance.Value, LayerMask.GetMask("Level")))
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
