using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    public Color Color;

    private new Light light;
    private float maxTime = 1f;
    private float startTime;
    private float endTime;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        light.color = Color;

        //var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //spriteRenderer.color = Color;

        var particleSystem = GetComponentInChildren<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = Color;

        startTime = Time.time;
        endTime = startTime + maxTime;

        Destroy(gameObject, maxTime);
    }

    void Update()
    {
        light.intensity = 1 - Mathf.InverseLerp(startTime, endTime, Time.time);
    }
}
