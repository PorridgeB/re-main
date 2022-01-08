using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

// Find cover position
public class FindCover : Action
{
	public SharedVector3 CoverPosition;

	public override void OnStart()
	{
	}

	public override TaskStatus OnUpdate()
	{
		var memory = GetComponent<Memory>();

		var observation = memory.WithTag("Cover");

		if (observation == null)
		{
			return TaskStatus.Failure;
		}

		CoverPosition.Value = observation.Who.transform.position;

		return TaskStatus.Success;
	}
}