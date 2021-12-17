using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Common")]
public class LastActivated : Conditional
{
	public SharedFloat Duration = 1f;

    private float lastTimeActivated = -1f;

    public override TaskStatus OnUpdate()
	{
        // Always activate on the first call
        if (lastTimeActivated < -1f || Time.time - lastTimeActivated > Duration.Value)
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