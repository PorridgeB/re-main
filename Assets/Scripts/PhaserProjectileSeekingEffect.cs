using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectileSeekingEffect : PhaserProjectileEffect
{
    [Tooltip("Maximum angle the projectile is allowed to turn within a second when seeking a target (in deg/s)")]
    public float MaxAngularVelocity = 70;
    [Tooltip("Maximum distance that the projectile searches for nearby targets")]
    public float TargetMaxDistance = 5;
    [Tooltip("Maximum arc angle between the projectile's direction of travel and direction to target for seeking")]
    [Range(0, 180)]
    public float TargetMaxArcAngle = 60;

    private void Update()
    {
        var closestTarget = FindClosestTarget(TargetMaxDistance, TargetMaxArcAngle);
        if (closestTarget)
        {
            var targetPosition = new Vector3(closestTarget.transform.position.x, 0, closestTarget.transform.position.z);
            var targetDirection = (targetPosition - transform.position).normalized;

            var maxRadiansDelta = Mathf.Deg2Rad * MaxAngularVelocity * Time.deltaTime;
            projectile.Direction = Vector3.RotateTowards(projectile.Direction, targetDirection, maxRadiansDelta, 0);
        }
    }

    private GameObject FindClosestTarget(float Radius, float ArcAngle)
    {
        GameObject closestTarget = null;
        var closestTargetDistance = Mathf.Infinity;

        var colliders = Physics.OverlapSphere(transform.position, Radius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(projectile.Target))
            {
                var target = collider.gameObject;
                var targetPosition = new Vector3(target.transform.position.x, 0, target.transform.position.z);
                var targetDistance = (targetPosition - transform.position).sqrMagnitude;
                var targetDirection = (targetPosition - transform.position).normalized;

                if (Vector3.Angle(projectile.Direction, targetDirection) > ArcAngle)
                {
                    continue;
                }

                if (targetDistance < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = targetDistance;
                }
            }
        }

        return closestTarget;
    }
}
