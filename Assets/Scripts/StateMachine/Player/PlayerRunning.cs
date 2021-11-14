using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunning : State
{
    private float runSoundRange;
    private float runSoundPeriod;

    public override void Enter(List<string> message)
    {
    }

    public override void Process()
    {
        movement.SetVelocity(moveAction.ReadValue<Vector2>() * stats.ReadAttribute("Run Speed"));
        if (moveAction.ReadValue<Vector2>() == Vector2.zero)
        {
            stateMachine.ChangeTo("Idle", null);
        }
        else if (walkAction.triggered)
        {
            stateMachine.ChangeTo("Sneak", null);
        }
    }
}
