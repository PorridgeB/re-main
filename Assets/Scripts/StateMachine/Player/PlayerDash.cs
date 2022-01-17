using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : StateMachineBehaviour
{
    [SerializeField]
    private AnimationCurve speedCurve;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDistance;
    private float dashTimer;
    private float dashSoundRange;
    private Vector2 dashDirection;
    private Vector3 startPosition;
    private Vector3 otherSide;

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

    private void CheckotherSideClear()
    {
        otherSide = new Vector3(startPosition.x+(dashDirection.x*dashDistance), startPosition.y, startPosition.z+(dashDirection.y*dashDistance));
        RaycastHit hitA;
        RaycastHit hitB;
        Physics.Raycast(startPosition, (otherSide-startPosition), out hitA);
        Physics.Raycast(otherSide, (startPosition-otherSide), out hitB);
        Debug.Log(hitA.collider.name + " : " + hitA.point + ", " + hitB.collider.name + " : " + hitB.point);
        if (hitA.point == hitB.point)
        {
            Debug.Log("Hit the same point");
        }
        else if (hitB.collider.gameObject.layer != LayerMask.NameToLayer("Dashable"))
        {
            Debug.Log("Not dashable");
        }
        else
        {
            otherSide = hitB.point;
            SetCollisionWithDashable(false);
            return;
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ignore collision with Enemies
        SetCollisionWithEnemies(false);
        startPosition = GameObject.Find("Player").transform.position + Vector3.up / 2;



        dashTimer = 0;
        dashDirection = new Vector2(animator.GetFloat("VelX"), animator.GetFloat("VelY"));

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical")).normalized;
        }

        CheckotherSideClear();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;
        PlayerController.instance.Dash(dashDirection * dashSpeed * speedCurve.Evaluate(dashTimer));

        if (Vector3.Distance(PlayerController.instance.transform.position, otherSide) < 0.1f){
            Debug.Log("reached other side");
            SetCollisionWithDashable(true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Enable collision with Enemies
        SetCollisionWithEnemies(true);
        SetCollisionWithDashable(true);

        PlayerController.instance.Stop();
    }

    
}
