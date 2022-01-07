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
		return TaskStatus.Success;
	}
}