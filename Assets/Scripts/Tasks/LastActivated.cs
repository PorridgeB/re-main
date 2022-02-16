using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Common")]
public class LastActivated : Conditional
{
	public SharedFloat Duration = 1;

    private float lastTimeActivated = -1;

    public override TaskStatus OnUpdate()
	{
        // Always activate on the first call
        if (lastTimeActivated < 0 || Time.time - lastTimeActivated > Duration.Value)
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