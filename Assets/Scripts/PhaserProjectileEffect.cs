using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectileEffect : MonoBehaviour
{
    protected PhaserProjectile projectile;

    private void Awake()
    {
        projectile = GetComponent<PhaserProjectile>();
    }
}
