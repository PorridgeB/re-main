using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Enemies/Drone")]
public class Charge : Action
{
	public SharedFloat Duration;
	public SharedFloat Speed;
	public SharedTransform Target;

	private NavMeshAgent agent;
	private ParticleSystem ghostTrail;
	private float startTime;

	public override void OnStart()
	{
		agent = gameObject.GetComponentInChildren<NavMeshAgent>();
		ghostTrail = gameObject.GetComponentInChildren<ParticleSystem>();

		startTime = Time.time;

		ghostTrail.Play();
	}

	public override TaskStatus OnUpdate()
	{
		agent.speed = Speed.Value;
		agent.destination = Target.Value.position;

		return Time.time - startTime > Duration.Value ? TaskStatus.Success : TaskStatus.Running;
	}

	public override void OnEnd()
	{
		agent.speed = 0;

		ghostTrail.Stop();
	}
}