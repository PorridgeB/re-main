using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : State
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float duration;
    private Timer durationTimer;
    private float dashSoundRange;
    private Vector2 dashDirection;

    public override void Enter(List<string> message)
    {
        dashDirection = movement.GetVelocity().normalized;
        durationTimer = new Timer(duration);
    }

    public override void PhysicsProcess()
    {
        if (durationTimer.Finished)
        {
            stateMachine.ChangeTo("Idle", null);
        }
        movement.SetVelocity(dashDirection* dashSpeed);
    }
}
