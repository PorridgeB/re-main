using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Reposition : Action
{
	public SharedGameObject Target;
	public SharedFloat Speed;
	public SharedFloat Duration;
	public SharedAnimationCurve SpeedCurve;

	private NavMeshAgent agent;
	private float startTime;
	private bool clockwise;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();

		startTime = Time.time;
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

		float time = (Time.time - startTime) / Duration.Value;

		agent.Move(perpDirectionToTarget * Speed.Value * SpeedCurve.Value.Evaluate(time) * Time.deltaTime);

		if (Time.time - startTime > Duration.Value)
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