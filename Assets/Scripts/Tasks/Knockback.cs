using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Knockback : Action
{
	public SharedFloat Duration = 0.8f;
	public SharedFloat Speed = 8f;
	public SharedAnimationCurve SpeedCurve;

	private float timer = 0f;

	private NavMeshAgent agent;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
		//agent.updatePosition = false;

		timer = 0f;
	}

	public override TaskStatus OnUpdate()
	{
		timer += Time.deltaTime;

		var t = 1 - timer / Duration.Value;

		var hitDirection = GetComponent<Enemy>().hitDirection;

		var hitDirection2 = hitDirection;
		hitDirection2.y = 0;

		var agent = GetComponent<NavMeshAgent>();

		var speed = Speed.Value * t * t;

		if (SpeedCurve != null)
        {
			speed = Speed.Value * SpeedCurve.Value.Evaluate(1 - t);
        }

		agent.Move(hitDirection2.normalized * speed * Time.deltaTime);

		return timer > Duration.Value ? TaskStatus.Success : TaskStatus.Running;
	}

    public override void OnEnd()
    {
	}
}