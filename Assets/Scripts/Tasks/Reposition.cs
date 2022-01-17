using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Reposition : Action
{
	public SharedGameObject Target;
	public SharedFloat Speed;
	public SharedFloat MinDuration;
	public SharedFloat MaxDuration;
	public SharedAnimationCurve SpeedCurve;

	private NavMeshAgent agent;
	private float startTime;
	private float duration;
	private bool clockwise;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();

		startTime = Time.time;
		duration = Random.Range(MinDuration.Value, MaxDuration.Value);
		clockwise = Random.value > 0.5f;
	}

	public override TaskStatus OnUpdate()
	{
		if (Target.Value == null)
		{
			return TaskStatus.Failure;
		}

		var directionToTarget = (Target.Value.transform.position - transform.position).normalized;
		directionToTarget.y = 0;
		directionToTarget.Normalize();

		var perpDirectionToTarget = new Vector3(-directionToTarget.z, 0, directionToTarget.x) * (clockwise ? -1 : 1);

		float time = (Time.time - startTime) / duration;

		agent.Move(perpDirectionToTarget * Speed.Value * SpeedCurve.Value.Evaluate(time) * Time.deltaTime);

		if (Time.time - startTime > duration)
		{
			return TaskStatus.Success;
		}

		return TaskStatus.Running;
	}

	public override void OnEnd()
	{
		agent.speed = 0;
	}
}