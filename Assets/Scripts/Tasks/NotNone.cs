using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Common")]
public class NotNone : Conditional
{
    public SharedGameObject Object;

    public override TaskStatus OnUpdate()
    {
        return Object.Value != null ? TaskStatus.Success : TaskStatus.Failure;
    }
}
