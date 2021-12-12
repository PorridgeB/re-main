using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Detonate : Action
{
	public override void OnStart()
	{
		var gnat = GetComponent<Gnat>();

		gnat.Detonate();
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}