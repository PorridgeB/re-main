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
        var distance = Vector3.Distance(Target.Value.transform.position, transform.position);

        if (distance > SightDistance.Value)
        {
            return TaskStatus.Failure;
        }
        
        var rayDistance = Mathf.Min(SightDistance.Value, distance);
        if (!Physics.Raycast(transform.position + Vector3.up, direction, out _, distance, LayerMask.GetMask("Level")))
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
