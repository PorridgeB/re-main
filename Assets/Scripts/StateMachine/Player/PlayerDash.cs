using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : State
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private Timer durationTimer;
    private float dashSoundRange;
    private Vector2 dashDirection;

    public override void Enter(List<string> message)
    {
        Debug.Log("Dashing");
        durationTimer.Reset();
        dashDirection = movement.GetVelocity().normalized;
    }

    public override void Process()
    {
        if (durationTimer.Finished)
        {
            stateMachine.ChangeTo("Idle", null);
        }
        if (meleeAction.triggered)
        {
            stateMachine.ChangeTo("DashAttack", null);
        }
    }

    public override void PhysicsProcess()
    {
        movement.SetVelocity(dashDirection* dashSpeed);
    }
}
