using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : State
{
    [SerializeField]
    private GameObject attackField;
    [SerializeField]
    private Timer durationTimer;

    private GameObject attackFieldInstance;

    public override void Enter(List<string> message)
    {
        durationTimer.Reset();
        attackFieldInstance = Instantiate(attackField);
        attackFieldInstance.transform.position = new Vector3(transform.position.x + (controller.GetFacing().x*5), transform.position.y + (controller.GetFacing().y*5), 0);
        attackFieldInstance.transform.LookAt(transform);
    }


    public override void Process()
    {
        attackFieldInstance.transform.position = new Vector3(transform.position.x + (controller.GetFacing().x * 5), transform.position.y + (controller.GetFacing().y * 5), 0);

        if (durationTimer.Finished)
        {
            stateMachine.ChangeTo("Idle", null);
        }
        if (dashAction.triggered)
        {
            stateMachine.ChangeTo("DashAttack", null);
        }
    }

    public override void Exit()
    {
        Destroy(attackFieldInstance);
        attackFieldInstance = null;
    }
}
