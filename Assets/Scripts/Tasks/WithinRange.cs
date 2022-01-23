using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Common")]
public class WithinRange : Conditional
{
    public SharedFloat Distance;

    public SharedGameObject Target;

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            return TaskStatus.Failure;
        }

        var distanceToTarget = Vector3.Distance(Target.Value.transform.position, transform.position);

        return distanceToTarget < Distance.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
