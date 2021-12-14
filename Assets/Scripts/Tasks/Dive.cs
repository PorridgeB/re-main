using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Dive : Action
{
	private Animator animator;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetTrigger("Dive");
	}

	public override TaskStatus OnUpdate()
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ? TaskStatus.Success : TaskStatus.Running;
	}

	public override void OnEnd()
    {
	}
}