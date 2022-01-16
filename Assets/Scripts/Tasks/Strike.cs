using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Strike : Action
{
	public SharedGameObject Target;
	public SharedFloat Speed;
	public SharedFloat Duration;
	public SharedFloat AttackDistance;

	private NavMeshAgent agent;
	private Enemy enemy;
	private float startTime;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
		enemy = GetComponent<Enemy>();

		startTime = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		if (Target.Value == null)
        {
			return TaskStatus.Failure;
        }

		var distanceToTarget = Vector3.Distance(Target.Value.transform.position, transform.position);

		var directionToTarget = (Target.Value.transform.position - transform.position).normalized;
		directionToTarget.y = 0;
		directionToTarget.Normalize();

		agent.Move(directionToTarget * Speed.Value * Time.deltaTime);

		if (distanceToTarget < AttackDistance.Value || Time.time - startTime > Duration.Value)
        {
			enemy.DoAttack();

			return TaskStatus.Success;
        }

		return TaskStatus.Running;
	}
}