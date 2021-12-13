using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class WithinSight : Conditional
{
    public SharedFloat SightDistance;
    //public LayerMask IgnoreMask;
    public SharedGameObject Target;

    public override TaskStatus OnUpdate()
    {
        RaycastHit raycastHit;

        var direction = (PlayerController.instance.transform.position - transform.position).normalized;

        var playerMask = LayerMask.GetMask("Player");
        var levelMask = LayerMask.GetMask("Level");

        if (Physics.Raycast(transform.position, direction, out raycastHit, SightDistance.Value, playerMask | levelMask))
        {
            if (raycastHit.collider.CompareTag("Player"))
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}
