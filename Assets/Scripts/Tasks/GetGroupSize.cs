using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Enemies")]
public class GetGroupSize : Action
{
	public SharedInt StoreValue;

	public override void OnStart()
	{
		var enemy = GetComponent<Enemy>();

		StoreValue.Value = enemy.group != null ? enemy.group.Size : 0;
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}