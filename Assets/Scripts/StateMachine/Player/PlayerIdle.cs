using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdle : State
{

    public override void Enter(List<string> message)
    {

    }

    public override void Process()
    {
        if (moveAction.ReadValue<Vector2>() != Vector2.zero)
        {
            if (walkAction.phase == InputActionPhase.Started)
            {
                stateMachine.ChangeTo("Sneak", null);
            }
            else
            {
                stateMachine.ChangeTo("Running", null);
            }
            
        }
    }
}
