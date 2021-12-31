using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : StateMachineBehaviour
{
    [SerializeField]
    private AnimationCurve speedCurve;
    [SerializeField]
    private float dashSpeed;
    private float dashTimer;
    private float dashSoundRange;
    private Vector2 dashDirection;

    private void SetCollisionWithEnemies(bool enabled)
    {
        var enemiesLayer = LayerMask.NameToLayer("Enemies");
        var playerLayer = LayerMask.NameToLayer("Player");

        Physics.IgnoreLayerCollision(playerLayer, enemiesLayer, !enabled);
    }

    private void SetCollisionWithDashable(bool enabled)
    {
        var dashableLayer = LayerMask.NameToLayer("Dashable");
        var playerLayer = LayerMask.NameToLayer("Player");
        Physics.IgnoreLayerCollision(playerLayer, dashableLayer, !enabled);
    }

    private void CheckEndpointClear()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        for (int i = 1; i < 6; i++)
        {
            Vector3 origin = playerPos + new Vector3(dashDirection.x, 0, dashDirection.y) * (i + .5f);
            Collider[] cols = Physics.OverlapBox(origin, new Vector3(0.1f, 0.1f, 0.1f));
            foreach (Collider c in cols)
            {
                if (c.gameObject.layer == LayerMask.NameToLayer("Dashable"))
                {
                    Debug.Log("hit dashable on " + i);
                    continue;
                }
            }
            SetCollisionWithDashable(false);
            return;
        }
        
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ignore collision with Enemies
        SetCollisionWithEnemies(false);
        



        dashTimer = 0;
        dashDirection = new Vector2(animator.GetFloat("VelX"), animator.GetFloat("VelY"));

        CheckEndpointClear();

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical")).normalized;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;
        PlayerController.instance.Dash(dashDirection * dashSpeed * speedCurve.Evaluate(dashTimer));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Enable collision with Enemies
        SetCollisionWithEnemies(true);
        SetCollisionWithDashable(true);

        PlayerController.instance.Stop();
    }
}
