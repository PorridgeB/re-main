using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialRangedAttack : StateMachineBehaviour
{
    public GameObject Projectile;
    public float Speed = 10f;
    public float Damage = 10f;
    public int ProjectileCount = 3;
    public float SpreadAngle = 45f;

    private IEnumerator shootCoroutine;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public IEnumerator Shoot(Animator animator)
    {
        var position = new Vector3(animator.transform.position.x, 0.5f, animator.transform.position.z);

        for (int i = 0; i < ProjectileCount; i++)
        {
            Projectile projectile = Instantiate(Projectile).GetComponent<Projectile>();

            var damageSource = projectile.GetComponent<DamageSource>();
            damageSource.source = PlayerController.instance.gameObject;
            damageSource.AddInstance(new DamageInstance { value = Damage, source = PlayerController.instance.gameObject });

            var offsetAngle = i * (SpreadAngle / (ProjectileCount - 1)) - SpreadAngle / 2;

            var direction = Quaternion.Euler(0, 0, offsetAngle) * PlayerController.instance.GetFacing();

            projectile.Shoot(position, direction, Speed);

            yield return new WaitForSeconds(2);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
