using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{
    public float Damage = 10f;
    [SerializeField]
    public GameObject AttackField;

    public float Duration = 2f;

    public float Distance = 0.5f;

    public string Trigger;
    
    private GameObject attackFieldInstance;
    private float timer = 0f;

    public override void OnStart()
    {
        var direction = (PlayerController.instance.transform.position - transform.position).normalized;

        attackFieldInstance = Object.Instantiate(AttackField, transform);

        DamageInstance damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Physical;
        damageInstance.source = gameObject;
        damageInstance.value = Damage;

        var damageSource = attackFieldInstance.GetComponent<DamageSource>();

        damageSource.AddInstance(damageInstance);
        damageSource.source = gameObject;

        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z).normalized;

        attackFieldInstance.transform.localPosition = forwardDirection * Distance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        timer = Duration;

        var animator = GetComponent<Animator>();
        animator.SetTrigger(Trigger);
    }

	public override TaskStatus OnUpdate()
	{
        timer -= Time.deltaTime;

        return timer < 0 ? TaskStatus.Success : TaskStatus.Running;
	}
    public override void OnEnd()
    {
        Object.Destroy(attackFieldInstance);
    }
}
