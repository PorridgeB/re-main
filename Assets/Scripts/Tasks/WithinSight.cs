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
    public SharedLayerMask Mask = new LayerMask { value = LayerMask.GetMask("Player") | LayerMask.GetMask("Level") };

    public override TaskStatus OnUpdate()
    {
        var direction = (Target.Value.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, SightDistance.Value, Mask.Value))
        {
            if (raycastHit.collider.gameObject == Target.Value)
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}
