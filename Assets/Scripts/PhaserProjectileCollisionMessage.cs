using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectileCollisionMessage
{
    public Collision Collision;
    public bool Impact { private set; get; } = true;

    public void DontImpact()
    {
        Impact = false;
    }
}
