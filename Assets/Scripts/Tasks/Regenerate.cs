using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Regenerate : Action
{
	public SharedFloat Health;
	public SharedFloat MaxHealth;
	// Health points gained per second
	public SharedFloat Rate;

	public override TaskStatus OnUpdate()
	{
		Health.Value += Rate.Value * Time.deltaTime;

		if (Health.Value >= MaxHealth.Value)
        {
			Health.Value = MaxHealth.Value;
			return TaskStatus.Success;
		}

		return TaskStatus.Running;
	}
}