using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSneak : State
{
    

    public override void Enter(List<string> message)
    {
    }

    public override void Process()
    {
        movement.SetVelocity(moveAction.ReadValue<Vector2>() * stats.ReadAttribute("Walk Speed"));
        if (moveAction.ReadValue<Vector2>() == Vector2.zero)
        {
            stateMachine.ChangeTo("Idle", null);
        }
        if (walkAction.phase == InputActionPhase.Waiting)
        {
            stateMachine.ChangeTo("Running", null);
        }
    }
}
