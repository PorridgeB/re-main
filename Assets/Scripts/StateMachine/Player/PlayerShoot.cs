using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : State
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Timer cooldown;

    public override void PhysicsProcess()
    {

        Projectile p = Instantiate(projectile).GetComponent<Projectile>();
        stateMachine.ChangeTo("Idle", null);
    }
}
