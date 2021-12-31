using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override void Fire()
    {
        Animator.SetTrigger("Melee");
    }

    public override void SpecialFire()
    {
    }
}
