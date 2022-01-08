using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Common")]
public class HasObserved : Conditional
{
	public SharedString Tag;
	public SharedFloat Since;

	public override TaskStatus OnUpdate()
	{
		var memory = GetComponent<Memory>();

		var observation = memory.FirstWithTag(Tag.Value);

		if (observation == null)
        {
			return TaskStatus.Failure;
        }

		if (Time.time - observation.When > Since.Value)
        {
			return TaskStatus.Failure;
		}

		return TaskStatus.Success;
	}
}