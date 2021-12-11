using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Knockback : Action
{
	public float Duration = 0.8f;
	public float Speed = 8f;

	private float timer = 0f;

	public override void OnStart()
	{
		timer = 0f;
	}

	public override TaskStatus OnUpdate()
	{
		timer += Time.deltaTime;

		var t = 1 - timer / Duration;

		var hitDirection = GetComponent<Enemy>().hitDirection;

		var agent = GetComponent<NavMeshAgent>();
		agent.Move(-hitDirection * Speed * t * t * Time.deltaTime);

		return timer > Duration ? TaskStatus.Success : TaskStatus.Running;
	}
}