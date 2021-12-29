using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    public Color Color;

    void Start()
    {
        var light = GetComponentInChildren<Light>();
        light.color = Color;

        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color;

        var particleSystem = GetComponentInChildren<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = Color;

        Destroy(gameObject, 1f);
    }

    void Update()
    {
    }
}
