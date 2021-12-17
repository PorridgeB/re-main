using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LastActivated : Conditional
{
	public float Duration = 1f;

    private float lastTimeActivated = -1f;

    public override TaskStatus OnUpdate()
	{
        // Always activate on the first call
        if (lastTimeActivated < -1f || Time.time - lastTimeActivated > Duration)
        {
            lastTimeActivated = Time.time;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
	}
}