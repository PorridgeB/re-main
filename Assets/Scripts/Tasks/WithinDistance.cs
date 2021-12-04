using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class WithinDistance : Conditional
{
    public SharedFloat Distance;

    public override TaskStatus OnUpdate()
    {
        var distance = (PlayerController.instance.transform.position - transform.position).sqrMagnitude;

        return distance < Distance.Value * Distance.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
