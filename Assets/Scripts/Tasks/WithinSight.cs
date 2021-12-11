using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class WithinSight : Conditional
{
    public SharedFloat SightDistance;
    public LayerMask IgnoreMask;
    public SharedGameObject Target;

    public override TaskStatus OnUpdate()
    {
        RaycastHit raycastHit;

        var direction = (PlayerController.instance.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out raycastHit, 30f, ~IgnoreMask))
        {
            if (raycastHit.collider.CompareTag("Player"))
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}
