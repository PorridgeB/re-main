using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : State
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Timer cooldownTimer;

    public override void Process()
    {
        if (cooldownTimer.Finished)
        {
            Projectile p = Instantiate(projectile).GetComponent<Projectile>();
            p.Shoot(transform.position, controller.GetFacing(), speed);
            cooldownTimer.Reset();
        }
        
        stateMachine.ChangeTo("Idle", null);
    }
}
