using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Charge : Action
{
	public SharedFloat Duration;
	public SharedFloat Speed;
	public SharedTransform Target;

	private NavMeshAgent agent;
	private float startTime;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();

		startTime = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		agent.speed = Speed.Value;
		agent.destination = PlayerController.instance.transform.position;

		return Time.time - startTime > Duration.Value ? TaskStatus.Success : TaskStatus.Running;
	}

	public override void OnEnd()
	{
		agent.speed = 0;
	}
}