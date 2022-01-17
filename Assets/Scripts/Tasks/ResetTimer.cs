using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetTimer : Action
{
	public SharedFloat Timer;

	public override void OnStart()
	{
		Timer.Value = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}