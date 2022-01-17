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

		var cover = observations.Where(x => IsValid(x.Who.transform.position)).OrderBy(x => Vector3.Distance(transform.position, x.Who.transform.position)).FirstOrDefault();
		if (cover == null)
        {
			return TaskStatus.Failure;
        }

		CoverPosition.Value = cover.Who.transform.position;

		return TaskStatus.Success;
	}

	private bool IsValid(Vector3 position)
    {
		return IsClose(position) && IsFree(position) && IsSafe(position);
	}
	
	private bool IsClose(Vector3 position)
    {
		return Vector3.Distance(transform.position, position) < MaxDistance.Value;
	}

	private bool IsFree(Vector3 position)
    {
		//var collider = GetComponent<CapsuleCollider>();
		//Physics.CheckCapsule(position, position + Vector3.up * collider.height, collider.radius);
		return !Physics.CheckBox(position + Vector3.up, Vector3.one * 0.25f);
	}

	private bool IsSafe(Vector3 position)
    {
		var target = Target.Value;
		if (target == null)
        {
			return true;
        }

		// A single check is not sufficient
		//return CheckSafe(position, target.transform.position);

		var checkRadius = 0.45f;
		var checkDirections = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
		foreach (var direction in checkDirections)
        {
			if (!CheckSafe(position + direction * checkRadius, target.transform.position))
            {
				return false;
            }
        }

		return true;
	}

	private bool CheckSafe(Vector3 from, Vector3 to)
    {
		var distance = Vector3.Distance(from, to);
		return Physics.Raycast(from + Vector3.up, (to - from).normalized, out _, distance, LayerMask.GetMask("Level"));
	}
}