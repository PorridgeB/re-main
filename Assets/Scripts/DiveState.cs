using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiveState : StateMachineBehaviour
{
    public float Damage = 10f;
    public float Duration = 1.2f;
    public float Speed = 12f;
    public AnimationCurve SpeedCurve;
    public GameObject AttackField;

    private const float AttackFieldDistance = 0.5f;

    private GameObject attackFieldInstance;
    private Vector3 direction;
    private float timer = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = (PlayerController.instance.transform.position - animator.transform.position).normalized;

        attackFieldInstance = Instantiate(AttackField, animator.transform);

        DamageInstance damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Physical;
        damageInstance.source = animator.gameObject;
        damageInstance.value = Damage;

        var damageSource = attackFieldInstance.GetComponent<DamageSource>();

        damageSource.source = animator.gameObject;
        damageSource.AddInstance(damageInstance);

        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z).normalized;

        attackFieldInstance.transform.localPosition = forwardDirection * AttackFieldDistance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        timer = Duration;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            animator.SetTrigger("Land");
            return;
        }

        var agent = animator.GetComponent<NavMeshAgent>();
        var time = 1 - timer / Duration;
        agent.Move(direction * Speed * SpeedCurve.Evaluate(time) * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(attackFieldInstance);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
