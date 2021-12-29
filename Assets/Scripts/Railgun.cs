using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : Weapon
{
    public override void Fire()
    {
        var shootBehaviour = Animator.GetBehaviour<PlayerRailgunShootShoot>();

        Animator.SetTrigger("RailgunShoot");
    }

    public override void SpecialFire()
    {
        Animator.SetTrigger("RailgunSpecial");
    }
}
