using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Knockback : Action
{
	public SharedFloat Duration = 0.8f;
	public SharedFloat Speed = 8f;
	public SharedAnimationCurve SpeedCurve;

	private NavMeshAgent agent;
	private Vector3 hitDirection;
	private float startTime;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
		hitDirection = GetComponent<Enemy>().hitDirection;
		startTime = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		var timePercentage = (Time.time - startTime) / Duration.Value;

		// Use a quadratic speed factor curve if none is provided
		var speedFactor = Mathf.Pow(1 - timePercentage, 2);

		if (SpeedCurve != null)
        {
			speedFactor = SpeedCurve.Value.Evaluate(timePercentage);
        }

		agent.Move(hitDirection * speedFactor * Speed.Value * Time.deltaTime);

		return Time.time - startTime > Duration.Value ? TaskStatus.Success : TaskStatus.Running;
	}
}