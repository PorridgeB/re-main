using System.Linq;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;

// Locate the closest valid cover position
[TaskCategory("Common")]
public class LocateCover : Action
{
	public SharedVector3 CoverPosition;
	public SharedFloat MaxDistance = 10;
	public SharedGameObject Target;

	public override TaskStatus OnUpdate()
	{
		var memory = GetComponent<Memory>();

		var observations = memory.WithTag("Cover");

		var cover = observations.Where(x => IsValid(x.Who.transform.position)).FirstOrDefault();

		if (cover == null)
        {
			return TaskStatus.Failure;
        }

		CoverPosition.Value = cover.Who.transform.position;

		return TaskStatus.Success;
	}

	private bool IsValid(Vector3 position)
    {
		// TODO: Check if can be reached
		// TODO: Check if it is in the direction away from the player/enemy
		return IsClose(position) && IsFree(position) && IsSafe(position);
	}
	
	private bool IsClose(Vector3 position)
    {
		return Vector3.Distance(transform.position, position) < MaxDistance.Value;
	}

	private bool IsFree(Vector3 position)
    {
		return !Physics.CheckBox(position + Vector3.up, Vector3.one * 0.25f);
	}

	private bool IsSafe(Vector3 position)
    {
		if (Target.Value == null)
        {
			return true;
        }

		var target = Target.Value;
		var from = target.transform.position + Vector3.up;
		var direction = (position - from).normalized;
		var maxDistance = 10;

		if (Physics.Raycast(from, direction, out RaycastHit hit, maxDistance, LayerMask.GetMask("Level")))
		{
			if (Vector3.Distance(hit.point, position) < 1)
			{
				return false;
			}
		}

		return true;
	}
}