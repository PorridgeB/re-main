using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Pursue : Action
{
	public SharedGameObject Target;
	public SharedFloat Speed;
	public SharedFloat MinDistance;

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

		//if (Vector3.Distance(transform.position, Target.Value.transform.position) < MinDistance.Value)
  //      {
		//	//return TaskStatus.Success;
		//	return TaskStatus.Failure;
  //      }

		agent.speed = Speed.Value;
		agent.destination = Target.Value.transform.position;

		return TaskStatus.Running;
	}

	public override void OnEnd()
	{
		agent.speed = 0;
	}
}