using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : StateMachineBehaviour
{
    [SerializeField]
    private GameObject attackField;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private AnimationCurve speedCurve;
    private float dashTimer;
    private Vector2 direction;
    private GameObject attackFieldInstance;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer = 0;
        direction = PlayerController.instance.GetFacing();
        attackFieldInstance = Instantiate(attackField);
        attackFieldInstance.transform.SetParent(PlayerController.instance.transform);
        attackFieldInstance.transform.position = new Vector3(direction.x*5, direction.y*5, 0);
        attackFieldInstance.transform.LookAt(animator.transform);
        Debug.Log(attackFieldInstance.transform.position);
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;
        PlayerController.instance.Dash(direction * dashSpeed * speedCurve.Evaluate(dashTimer));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(attackFieldInstance);
        attackFieldInstance = null;
        PlayerController.instance.Stop();
    }
}
