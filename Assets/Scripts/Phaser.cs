using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : Weapon
{
    public override void Fire()
    {
        Animator.SetTrigger("Ranged");
    }

    public override void SpecialFire()
    {
    }
}
