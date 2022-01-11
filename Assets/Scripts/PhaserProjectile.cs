using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserProjectile : MonoBehaviour
{
    [Tooltip("Direction of travel (normalized)")]
    public Vector3 Direction;
    [Tooltip("Speed of the projectile (in unit/s)")]
    public float Speed = 15;
    [Tooltip("Maximum distance the projectile can travel from its starting position before being destroyed")]
    public float Range = 25;
    [Tooltip("Maximum time the projectile can travel")]
    public float Lifespan = 8;
    [Tooltip("Width of the trail")]
    public float TrailWidth = 0.25f;
    [Tooltip("Colour of the light and the trail")]
    public Color Color = Color.white;
    [Tooltip("Time for the projectile to detect collisions again with the previous collider")]
    public float IgnoreCollisionTime = 0.5f;
    [Tooltip("GameObject tag that this projectile targets")]
    public string Target = "Enemy";
    [Tooltip("Prefab to create when the projectile has impacted with something")]
    public GameObject ImpactPrefab;
    [Tooltip("Who created the projectile")]
    public GameObject Source;

    private new Rigidbody rigidbody;
    private new Light light;
    private TrailRenderer trail;
    private new SphereCollider collider;
    private Vector3 startPosition;
    private float startTime;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        light = GetComponentInChildren<Light>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        light.color = Color;

        trail.startColor = Color;
        trail.endColor = new Color(Color.r, Color.g, Color.b, 0);

        trail.startWidth = TrailWidth;
        trail.endWidth = 0;

        startPosition = transform.position;
        startTime = Time.time;
    }

    private void Update()
    {
        rigidbody.velocity = Direction * Speed;

        if (Vector3.Distance(startPosition, transform.position) > Range)
        {
            Fizzle();
        }

        if (Time.time - startTime > Lifespan)
        {
            Fizzle();
        }
    }

    private IEnumerator TemporarilyIgnoreCollision(Collider other, float time)
    {
        Physics.IgnoreCollision(collider, other);
        yield return new WaitForSeconds(time);
        Physics.IgnoreCollision(collider, other, false);
    }

    public void Fizzle()
    {
        Destroy(gameObject);
    }

    public void Impact()
    {
        SendMessage("OnImpact", SendMessageOptions.DontRequireReceiver);
        
        CreateImpactEffect();

        // Destroy the projectile after some time has passed
        // so the trail renderer can continue
        rigidbody.detectCollisions = false;
        rigidbody.velocity = Vector3.zero;
        light.enabled = false;
        enabled = false;
        Destroy(gameObject, 1);
    }

    public void CreateImpactEffect()
    {
        if (ImpactPrefab != null)
        {
            var impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity).GetComponent<PhaserProjectileImpact>();
            impact.Color = Color;
            impact.Source = Source;
        }
    }

    public void IgnoreCollision(Collider other)
    {
        StartCoroutine(TemporarilyIgnoreCollision(other, IgnoreCollisionTime));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Source.tag))
        {
            Physics.IgnoreCollision(collider, collision.collider);
            return;
        }

        if (collision.gameObject != Source)
        {
            Impact();
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(Target))
    //    {
    //        if (PassthroughEnabled && Random.value < PassthroughChance && passthroughs++ < PassthroughMax)
    //        {
    //            CreateImpactEffect();
    //            IgnoreCollision(collision.collider);
    //        }
    //        else
    //        {
    //            if (SplitEnabled && Random.value < SplitChance)
    //            {
    //                Split(collision.collider);
    //            }

    //            Impact();
    //        }
    //    }
    //    else if (collision.gameObject.CompareTag("Room"))
    //    {
    //        if (RicochetEnabled && Random.value < RicochetChance && ricochets++ < RicochetMax)
    //        {
    //            var normal = collision.contacts[0].normal;
    //            Ricochet(normal);
    //        }
    //        else
    //        {
    //            Impact();
    //        }
    //    }
    //}
}
