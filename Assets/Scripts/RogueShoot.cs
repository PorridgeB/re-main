using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueShoot : StateMachineBehaviour
{
    public Color Color;
    public float ProjectileOffset = 0.5f;
    public float ProjectileSpeed = 15;
    public GameObject ProjectilePrefab;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var projectile = Instantiate(ProjectilePrefab).GetComponent<PhaserProjectile>();

        var target = PlayerController.instance;

        var targetPosition = target.transform.position;
        var targetVelocity = new Vector3(target.GetVelocity().x, 0, target.GetVelocity().y);

        // Find the relative position and velocities
        var relativePosition = targetPosition - animator.transform.position;
        relativePosition.y = 0;
        var relativeVelocity = targetVelocity;

        var deltaTime = AimAhead(relativePosition, relativeVelocity, ProjectileSpeed);
        
        // Variation for some fun
        deltaTime += Random.Range(0.025f, 0.035f);

        // If the time is negative, then we didn't get a solution.
        if (deltaTime > 0)
        {
            // Aim at the point where the target will be at the time of the collision.
            var futurePosition = targetPosition + targetVelocity * deltaTime;

            var projectileDirection = (futurePosition - animator.transform.position).normalized;

            projectile.transform.position = animator.transform.position + projectileDirection * ProjectileOffset;
            projectile.Direction = projectileDirection;
            projectile.Speed = ProjectileSpeed;
            projectile.Target = target.tag;
            projectile.Color = Color;
            projectile.Source = animator.gameObject;
        }
    }

    private static float AimAhead(Vector3 relativePosition, Vector3 relativeVelocity, float projectileSpeed)
    {
        var a = Vector3.Dot(relativeVelocity, relativeVelocity) - projectileSpeed * projectileSpeed;
        var b = 2f * Vector3.Dot(relativeVelocity, relativePosition);
        var c = Vector3.Dot(relativePosition, relativePosition);

        var discriminant = b * b - 4f * a * c;

        // If the discriminant is negative, then there is no solution
        return discriminant > 0 ? 2 * c / (Mathf.Sqrt(discriminant) - b) : -1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
