using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Dive : Action
{
	public float Duration = 1.2f;
	public float Damage = 10f;
	public float AttackFieldDistance = 0.5f;
	public SharedTransform Target;
	public GameObject AttackField;
	public float Speed = 6f;
	public AnimationCurve SpeedCurve;

	private Animator animator;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetTrigger("Dive");
	}

	public override TaskStatus OnUpdate()
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ? TaskStatus.Success : TaskStatus.Running;
	}

	public override void OnEnd()
    {
	}
}