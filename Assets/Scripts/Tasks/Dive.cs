using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Dive : Action
{
	public float Damage = 10f;
	public float Duration = 1f;
	public float Speed = 12f;
	public AnimationCurve SpeedCurve;
	public GameObject AttackField;
	public SharedTransform Target;

	private Animator animator;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetTrigger("Dive");

		var diveState = animator.GetBehaviour<DiveState>();
		diveState.Damage = Damage;
		diveState.Duration = Duration;
		diveState.Speed = Speed;
		diveState.SpeedCurve = SpeedCurve;
		diveState.AttackField = AttackField;
		diveState.Target = Target.Value;
	}

	public override TaskStatus OnUpdate()
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ? TaskStatus.Success : TaskStatus.Running;
	}
}