using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public Animator Animator;

    public virtual void Fire()
    {
    }

    public virtual void SpecialFire()
    {
    }

    public virtual void DashFire()
    {
    }
}
