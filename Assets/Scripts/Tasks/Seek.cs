using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Seek : Action
{
	public SharedFloat Speed;

	private float oldSpeed;
	private NavMeshAgent agent;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();

		oldSpeed = agent.speed;
	}

	public override TaskStatus OnUpdate()
	{
		agent.speed = Speed.Value;
		agent.destination = PlayerController.instance.transform.position;

		return TaskStatus.Running;
	}

    public override void OnEnd()
    {
		agent.speed = oldSpeed;

	}
}