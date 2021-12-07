using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{
    public float Damage = 10f;
	public GameObject AttackField;

    private GameObject attackFieldInstance;
    private float timer = 0f;

    public override void OnStart()
    {
        var direction = (PlayerController.instance.transform.position - transform.position).normalized;

        attackFieldInstance = Object.Instantiate(AttackField);

        DamageInstance d = new DamageInstance();
        d.type = DamageType.Physical;
        d.source = gameObject;
        d.value = Damage;

        attackFieldInstance.GetComponent<DamageSource>().AddInstance(d);
        attackFieldInstance.transform.SetParent(transform);

        float distance = 0.5f;
        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.y);

        attackFieldInstance.transform.localPosition = forwardDirection * distance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        timer = 0.2f;
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
