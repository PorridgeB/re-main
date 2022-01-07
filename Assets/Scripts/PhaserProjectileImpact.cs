using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectileImpact : MonoBehaviour
{
    public Color Color;

    [HideInInspector]
    public GameObject Source;

    [SerializeField]
    private GameObject hurtbox;
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

        var damageSource = hurtbox.GetComponent<DamageSource>();
        damageSource.source = Source;
        damageSource.AddInstance(new DamageInstance { source = Source, type = DamageType.Energy, value = 1 });

        Destroy(hurtbox, 0.1f);
    }

    void Update()
    {
        light.intensity = 1 - Mathf.InverseLerp(startTime, endTime, Time.time);
    }
}
