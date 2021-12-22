using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Common")]
public class WithinDistance : Conditional
{
    public SharedFloat Distance;

    public SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        var distanceToTarget = (Target.Value.position - transform.position).magnitude;

        return distanceToTarget < Distance.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
