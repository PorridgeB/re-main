using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Flank : Action
{
    public SharedFloat Speed;
    // 1 for a perfect orbit around target, 0 for moving directly towards target
    public SharedFloat Orbit;
    public SharedTransform Target;

    private NavMeshAgent agent;
    // Orbit in a clockwise rotation
    private bool clockwise = false;

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();

        clockwise = Random.value > 0.5f;
    }

    public override TaskStatus OnUpdate()
    {
        var targetDirection = (Target.Value.position - transform.position).normalized;
        var targetDirection2D = new Vector2(targetDirection.x, targetDirection.z).normalized;

        var orbitDirection2D = Vector2.Perpendicular(targetDirection2D) * (clockwise ? -1 : 1);

        var direction2D = Vector2.Lerp(targetDirection2D, orbitDirection2D, Orbit.Value).normalized;

        var velocity2D = direction2D * Speed.Value;

        agent.Move(new Vector3(velocity2D.x, 0, velocity2D.y) * Time.deltaTime);

        return TaskStatus.Running;
    }
}