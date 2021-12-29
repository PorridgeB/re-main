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
    [SerializeField]
    private GameObject cube;

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
        Vector3 origin = GameObject.Find("Player").transform.position + new Vector3(dashDirection.x, 0, dashDirection.y) * 5.5f;
        Collider[] cols = Physics.OverlapBox(origin, new Vector3(0.1f, 0.1f, 0.1f));
        foreach (Collider c in cols)
        {
            Debug.Log(c.name);
            if (c.gameObject.layer == LayerMask.NameToLayer("Dashable"))
            {
                Debug.Log("cant dash here");
                return;
            }
        }
        SetCollisionWithDashable(false);
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
