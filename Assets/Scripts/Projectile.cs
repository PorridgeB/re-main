using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed = 10f;
    public float MaximumDistance = 25f;
    public float Size = 0.25f;
    public Color Color = Color.white;

    private new Rigidbody rigidbody;
    private new Light light;
    private TrailRenderer trailRenderer;
    private SphereCollider sphereCollider;
    private Vector3 startPosition;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        light = GetComponentInChildren<Light>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        sphereCollider = GetComponent<SphereCollider>();

        light.color = Color;

        trailRenderer.startColor = Color;
        trailRenderer.endColor = new Color(Color.r, Color.g, Color.b, 0);

        trailRenderer.startWidth = Size;
        trailRenderer.endWidth = 0;

        sphereCollider.radius = Size;

        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(Direction.x, 0, Direction.y) * Speed;

        rigidbody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

        if (Vector3.Distance(startPosition, transform.position) > MaximumDistance)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
