using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TimerExpired : Conditional
{
	public SharedFloat Timer;
	public SharedFloat Duration;

	public override TaskStatus OnUpdate()
	{
		return Time.time - Timer.Value > Duration.Value ? TaskStatus.Success : TaskStatus.Failure;
	}
}