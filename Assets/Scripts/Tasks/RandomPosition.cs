using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RandomPosition : Action
{
    public SharedVector3 Position;

    public override void OnStart()
    {
        Position = new Vector3(10, 0, 10);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
