using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Dive : Action
{
	public float Duration = 1.2f;
	public float Damage = 10f;
	public float AttackFieldDistance = 0.5f;
	public SharedTransform Target;
	public GameObject AttackField;
	public float Speed = 6f;
	public AnimationCurve SpeedCurve;

	private Vector3 direction;
	private float timer;
	private GameObject attackFieldInstance;

	public override void OnStart()
	{
		//var animator = GetComponent<Animator>();
		//animator.SetTrigger("Dive");

		direction = (PlayerController.instance.transform.position - transform.position).normalized;

		attackFieldInstance = Object.Instantiate(AttackField, transform);

		DamageInstance damageInstance = new DamageInstance();
		damageInstance.type = DamageType.Physical;
		damageInstance.source = gameObject;
		damageInstance.value = Damage;

		var damageSource = attackFieldInstance.GetComponent<DamageSource>();

		damageSource.AddInstance(damageInstance);
		damageSource.source = gameObject;

		Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z).normalized;

		attackFieldInstance.transform.localPosition = forwardDirection * AttackFieldDistance;
		attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

		timer = Duration;
	}

	public override TaskStatus OnUpdate()
	{
		timer -= Time.deltaTime;
		if (timer < 0)
        {
			return TaskStatus.Success;
        }

		var agent = GetComponent<NavMeshAgent>();

		var time = 1 - timer / Duration;

		agent.Move(direction * Speed * SpeedCurve.Evaluate(time) * Time.deltaTime);

		return TaskStatus.Running;
	}

	public override void OnEnd()
    {
		Object.Destroy(attackFieldInstance);
	}
}