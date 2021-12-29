using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunBeam : MonoBehaviour
{
    public Color Color;
    public float MaxDistance = 15f;
    public float HitOffset = -1f;
    public float Width = 0.5f;
    public float WidthVariation = 0.1f;
    public float WidthFrequency = 10f;
    public LayerMask Mask;
    public GameObject Light;
    public int MaxLights = 8;
    public float LightSpacing = 2f;
    public GameObject Impact;
    public ParticleSystem BeamParticleSystem;

    private LineRenderer line;
    private GameObject[] lights;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();

        line.startColor = Color;
        line.endColor = Color;

        var beamMain = BeamParticleSystem.main;
        beamMain.startColor = Color;

        var light = Light.GetComponent<Light>();
        light.color = Color;

        var impactParticleSystem = Impact.GetComponent<ParticleSystem>();
        var impactMain = impactParticleSystem.main;
        impactMain.startColor = Color;

        SetupLights();
    }

    void SetupLights()
    {
        lights = new GameObject[MaxLights];
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i] = Instantiate(Light, transform);
            lights[i].transform.localPosition = new Vector3(0, 0.5f, (i + 1) * LightSpacing);

            var light = lights[i].GetComponent<Light>();
            light.color = Color;
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var distance = MaxDistance;

        RaycastHit info;
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out info, MaxDistance, Mask))
        {
            distance = info.distance + HitOffset;
        }

        // Update line renderer
        line.SetPosition(1, new Vector3(0, 0, distance));

        // Update particle system
        var shape = BeamParticleSystem.shape;
        shape.scale = new Vector3(1, 1, distance);
        shape.position = new Vector3(0, 0.5f, 0.5f + distance / 2);

        // Update lights along beam
        var numLights = Mathf.Min(Mathf.CeilToInt(distance / LightSpacing), MaxLights);
        for (int i = 0; i < lights.Length; i++)
        {
            var light = lights[i].GetComponent<Light>();
            light.enabled = i <= numLights;
        }

        // Update end bit
        Impact.transform.localPosition = new Vector3(0, 0.5f, distance);
    }

    void Update()
    {
        var width = Width + WidthVariation * Mathf.Sin(2f * Mathf.PI * Time.time * WidthFrequency);

        line.startWidth = width;
        line.endWidth = width;
    }
}
