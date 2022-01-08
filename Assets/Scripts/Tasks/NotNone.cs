using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotNone : Conditional
{
    public SharedGameObject Target;

    public override TaskStatus OnUpdate()
    {
        return Target.Value != null ? TaskStatus.Success : TaskStatus.Failure;
    }
}
