using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RandomPosition : Action
{
    public SharedVector2 Position;

    public override void OnStart()
    {
        Position = new Vector2(10, 0);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
