using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMeleeAttack : StateMachineBehaviour
{
    [SerializeField]
    private Attack nextAttack;
    [SerializeField]
    private GameObject attackField;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private AnimationCurve speedCurve;

    [SerializeField]
    private UnityEvent OnPlayerMeleeAttack;
    private float dashTimer;
    private Vector2 direction;
    private GameObject attackFieldInstance;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnPlayerMeleeAttack.Invoke();

        dashTimer = 0;
        direction = PlayerController.instance.GetFacing();
        
        
        attackFieldInstance = Instantiate(attackField);
        DamageSource script = attackFieldInstance.GetComponent<DamageSource>();
        foreach (DamageInstance d in nextAttack.damageInstances)
        {
            script.AddInstance(d);
            
        }
        nextAttack.damageInstances.Clear();
        attackFieldInstance.transform.SetParent(PlayerController.instance.transform);

        float distance = 0.5f;
        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.y);

        attackFieldInstance.transform.localPosition = forwardDirection * distance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        //Vector3 targ = animator.transform.position;
        //targ.z = 0f;

        //Vector3 objectPos = attackFieldInstance.transform.position;
        //targ.x = targ.x - objectPos.x;
        //targ.y = targ.y - objectPos.y;

        //float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        //attackFieldInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        //attackFieldInstance.transform.LookAt(animator.transform);
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;
        PlayerController.instance.Dash(direction * dashSpeed * speedCurve.Evaluate(dashTimer));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(attackFieldInstance);
        attackFieldInstance = null;
        PlayerController.instance.Stop();
    }
}
