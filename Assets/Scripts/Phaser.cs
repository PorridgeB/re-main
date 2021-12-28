using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : Weapon
{
    public override void Fire()
    {
        var shootBehaviour = Animator.GetBehaviour<PlayerShoot>();

        Animator.SetTrigger("PhaserShoot");
    }

    public override void SpecialFire()
    {
        var flurryBehaviour = Animator.GetBehaviour<PlayerPhaserFlurry>();
        flurryBehaviour.ProjectileCount = 4;
        flurryBehaviour.SpreadAngle = 45f;

        Animator.SetTrigger("PhaserFlurry");
    }
}
