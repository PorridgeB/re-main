using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Common")]
public class MaintainDistance : Action
{
	public SharedGameObject Target;
	public SharedFloat Speed;
	public SharedFloat MinDistance;
	public SharedFloat MaxDistance;

	private NavMeshAgent agent;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public override TaskStatus OnUpdate()
	{
		if (Target.Value == null)
		{
			return TaskStatus.Failure;
		}

		agent.destination = Target.Value.transform.position;

		var directionToTarget = (Target.Value.transform.position - transform.position).normalized;
		directionToTarget.y = 0;
		directionToTarget.Normalize();

		//var distance = Mathf.Min(MaxDistance.Value, Vector3.Distance(Target.Value.transform.position, transform.position));

		//// If we cannot see the target, move towards them anyway, disregarding distance
		//if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out _, distance, LayerMask.GetMask("Level")))
		//{
		//	agent.speed = Speed.Value;
		//	return TaskStatus.Running;
		//}

		var distanceToTarget = Vector3.Distance(Target.Value.transform.position, transform.position);

		if (distanceToTarget < MinDistance.Value)
		{
			agent.Move(-directionToTarget * Speed.Value * Time.deltaTime);
			agent.speed = 0;
		}
		else if (distanceToTarget > MaxDistance.Value)
		{
			agent.speed = Speed.Value;
		}
		else
        {
			agent.speed = 0;
		}


		return TaskStatus.Running;
	}

	public override void OnEnd()
	{
		agent.speed = 0;
	}
}