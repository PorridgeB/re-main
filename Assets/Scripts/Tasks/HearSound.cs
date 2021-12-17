using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Common")]
public class HearSound : Conditional
{
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Failure;
    }
}
