using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LastActivated : Conditional
{
	public float Duration = 1f;

    private float lastTimeActivated = 0f;

    public override TaskStatus OnUpdate()
	{
        if (Time.time - lastTimeActivated > Duration)
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