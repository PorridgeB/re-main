using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HearSound : Conditional
{
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Failure;
    }
}
