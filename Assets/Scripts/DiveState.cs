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
    public Transform Target;

    private const float AttackFieldDistance = 0.5f;

    private GameObject attackFieldInstance;
    private Vector3 direction;
    private float startTime;
    private NavMeshAgent agent;
    private Rigidbody rigidbody;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = (Target.position - animator.transform.position).normalized;

        attackFieldInstance = Instantiate(AttackField, animator.transform);

        DamageInstance damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Physical;
        damageInstance.source = animator.gameObject;
        damageInstance.value = Damage;

        var damageSource = attackFieldInstance.GetComponent<DamageSource>();

        damageSource.source = animator.gameObject;
        damageSource.AddInstance(damageInstance);

        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z).normalized;

        attackFieldInstance.transform.localPosition = new Vector3(0, 0.5f, 0) + forwardDirection * AttackFieldDistance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        startTime = Time.time;

        // Disable the NavMeshAgent's synchronisation with transform.position,
        // so we can move the rigidbody using the MovePosition method
        agent = animator.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;

        rigidbody = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time - startTime > Duration)
        {
            animator.SetTrigger("Land");
        }

        var time = (Time.time - startTime) / Duration;
        var velocity = direction * Speed * SpeedCurve.Evaluate(time);
        rigidbody.MovePosition(rigidbody.position + 5 * velocity * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var rigidbodyPosition = rigidbody.position;
        agent.updatePosition = true;
        agent.Warp(rigidbodyPosition);

        Destroy(attackFieldInstance);
    }
}
