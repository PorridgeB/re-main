using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Knockback : Action
{
	public float Duration = 0.8f;
	public float Speed = 8f;
	public AnimationCurve SpeedCurve;

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

		var t = 1 - timer / Duration;

		var hitDirection = GetComponent<Enemy>().hitDirection;

		var hitDirection2 = hitDirection;
		hitDirection2.y = 0;

		//var rigidbody = GetComponent<Rigidbody>();
		//rigidbody.MovePosition(transform.position + hitDirection2.normalized * 20f * Time.deltaTime);
		//rigidbody.MovePosition(transform.position + hitDirection2 * 30f * Time.deltaTime);

		var agent = GetComponent<NavMeshAgent>();
		//agent.speed = 1;
		//agent.Move(-hitDirection);

		var speed = Speed * t * t;

		if (SpeedCurve != null)
        {
			speed = Speed * SpeedCurve.Evaluate(1 - t);
        }

		agent.Move(hitDirection2.normalized * speed * Time.deltaTime);

		return timer > Duration ? TaskStatus.Success : TaskStatus.Running;
	}

    public override void OnEnd()
    {
		//agent.updatePosition = true;
	}
}