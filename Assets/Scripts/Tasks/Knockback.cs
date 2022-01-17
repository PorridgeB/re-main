using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Enemies")]
public class Knockback : Action
{
	public SharedFloat ImpulseTime = 40;
	public SharedFloat FrictionCoefficient = 10;

	private Enemy enemy;
	private NavMeshAgent agent;
	private Vector3 direction;
	private float force;
	private float startTime;

	public override void OnStart()
	{
		enemy = GetComponent<Enemy>();
		agent = GetComponent<NavMeshAgent>();
		direction = enemy.HitDirection;
		force = enemy.HitForce;
		startTime = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		var initialVelocity = ImpulseTime.Value * force / enemy.Mass;
		var acceleration = -FrictionCoefficient.Value;
		var velocity = initialVelocity + acceleration * (Time.time - startTime);

		if (velocity < initialVelocity * 0.05f)
		{
			return TaskStatus.Success;
		}

		agent.Move(direction * velocity * Time.deltaTime);

		return TaskStatus.Running;
	}
}