using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Enemies/Drone")]
public class Dive : Action
{
	public SharedFloat Damage = 10f;
	public SharedFloat Duration = 1f;
	public SharedFloat Speed = 12f;
	public SharedAnimationCurve SpeedCurve;
	public SharedGameObject AttackField;
	public SharedTransform Target;

	private Animator animator;
	private Enemy enemy;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetTrigger("Dive");

		var diveState = animator.GetBehaviour<DiveState>();
		diveState.Damage = Damage.Value;
		diveState.Duration = Duration.Value;
		diveState.Speed = Speed.Value;
		diveState.SpeedCurve = SpeedCurve.Value;
		diveState.AttackField = AttackField.Value;
		diveState.Target = Target.Value;

		enemy = GetComponent<Enemy>();
		enemy.unstoppable = true;
	}

	public override TaskStatus OnUpdate()
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ? TaskStatus.Success : TaskStatus.Running;
	}

    public override void OnEnd()
    {
		enemy.unstoppable = false;
	}
}