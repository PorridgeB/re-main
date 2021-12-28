using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaserFlurry : StateMachineBehaviour
{
    public float Speed = 15f;
    public float Damage = 10f;
    public float Distance = 0.25f;
    public int ProjectileCount = 4;
    public float SpreadAngle = 45f;

    [SerializeField]
    private GameObject projectilePrefab;
    private Vector2 direction;
    private int previousShotCount = -1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = PlayerController.instance;

        direction = player.Facing;
        previousShotCount = -1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var shotCount = Mathf.FloorToInt(stateInfo.normalizedTime * ProjectileCount);

        if (shotCount != previousShotCount)
        {
            Shoot(shotCount);

            previousShotCount = shotCount;
        }
    }

    private void Shoot(int shotCount)
    {
        var angle = shotCount * (SpreadAngle / (ProjectileCount - 1)) - SpreadAngle / 2;
        var projectileDirection = Quaternion.Euler(0, 0, angle) * direction;

        var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();

        var damageSource = projectile.GetComponent<DamageSource>();
        damageSource.source = PlayerController.instance.gameObject;
        damageSource.AddInstance(new DamageInstance { value = Damage, source = PlayerController.instance.gameObject });

        var player = PlayerController.instance;

        projectile.transform.position = player.transform.position + new Vector3(player.Facing.x, 0, player.Facing.y) * Distance;
        projectile.Direction = projectileDirection;
        projectile.Speed = Speed;
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
