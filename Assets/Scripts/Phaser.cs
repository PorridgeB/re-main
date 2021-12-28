using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : Weapon
{
    public override void Fire()
    {
        var shootBehaviour = Animator.GetBehaviour<PlayerPhaserShoot>();

        Animator.SetTrigger("PhaserShoot");
    }

    public override void SpecialFire()
    {
        var flurryBehaviour = Animator.GetBehaviour<PlayerPhaserFlurry>();
        flurryBehaviour.ProjectileCount = 3;
        flurryBehaviour.SpreadAngle = 30f;

        Animator.SetFloat("PhaserFlurrySpeedMultiplier", 4f);

        Animator.SetTrigger("PhaserFlurry");
    }
}
