using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : StateMachineBehaviour
{
    [SerializeField]
    private AnimationCurve speedCurve;
    [SerializeField]
    private float dashSpeed;
    private float dashTimer;
    private float dashSoundRange;
    private Vector2 dashDirection;



    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer = 0;
        dashDirection = new Vector2(animator.GetFloat("VelX"), animator.GetFloat("VelY"));
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical")).normalized;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;
        PlayerController.instance.Dash(dashDirection * dashSpeed * speedCurve.Evaluate(dashTimer));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.instance.Stop();
    }
}
