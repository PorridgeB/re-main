using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool grounded;
    public LayerMask mask;

    private void SetCollisionWithDashable(bool enabled)
    {
        var dashableLayer = LayerMask.NameToLayer("Dashable");
        var playerLayer = LayerMask.NameToLayer("Player");
        Physics.IgnoreLayerCollision(playerLayer, dashableLayer, !enabled);
    }

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position+Vector3.up*1, Vector3.down), out hit);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                if (!grounded)
                {
                    SetCollisionWithDashable(true);
                }
                grounded = true;
                return;
            }
        }
        grounded = false;
    }
}
