using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : State
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    private float dashTimer;
    private float dashSoundRange;
    private Vector2 dashDirection;

    public override void Enter(List<string> message)
    {
        dashDirection = movement.GetVelocity().normalized;
        dashTimer = dashDuration;
    }

    public override void PhysicsProcess()
    {
        dashTimer -= Time.fixedDeltaTime;
        if (dashTimer < 0)
        {
            stateMachine.ChangeTo("Idle", null);
        }
        movement.SetVelocity(dashDirection* dashSpeed);
    }
}
