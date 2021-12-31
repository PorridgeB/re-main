using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Direction of travel (normalized)
    [HideInInspector]
    public Vector3 Direction;
    [Tooltip("Speed of the projectile (in unit/s)")]
    public float Speed = 10f;
    [Tooltip("Maximum distance the projectile can travel from its starting position before being destroyed")]
    public float Range = 25f;
    [Tooltip("Width of the trail")]
    public float Size = 0.25f;
    [Tooltip("Colour of the light and the trail")]
    public Color Color = Color.white;
    [Tooltip("Time for the projectile to detect collisions again with the previous collider")]
    public float IgnoreCollisionTime = 0.5f;
    [Tooltip("Prefab to create when the projectile has impacted with something")]
    public GameObject ImpactPrefab;

    [Header("Seeking")]
    [Tooltip("Enables the projectile to bend toward nearby enemies")]
    public bool SeekingEnabled = true;
    [Tooltip("Maximum angle the projectile is allowed to turn within a second when seeking an enemy (in deg/s)")]
    public float SeekingAngularVelocity = 70f;
    [Tooltip("Maximum distance that the projectile searches for nearby enemies")]
    public float SeekingTargetDistance = 5f;
    [Tooltip("Maximum arc angle between the projectile's direction of travel and direction to enemy for seeking")]
    [Range(0, 180)]
    public float SeekingTargetArcAngle = 60f;

    [Header("Ricochet")]
    [Tooltip("Enables the projectile to bounce when colliding with a wall")]
    public bool RicochetEnabled = true;
    [Tooltip("Chance of the projectile ricocheting when colliding with a wall")]
    [Range(0, 1)]
    public float RicochetChance = 0.75f;
    [Tooltip("Maximum number of times the projectile can ricochet before impacting")]
    public int RicochetMax = 4;

    [Header("Passthrough")]
    [Tooltip("Enables the projectile to hit and passthrough an enemy")]
    public bool PassthroughEnabled = true;
    [Tooltip("Chance of the projectile passing through an enemy when colliding with one")]
    [Range(0, 1)]
    public float PassthroughChance = 1f;
    [Tooltip("Maximum number of enemy collisions before the projectile is destroyed")]
    public int PassthroughMax = 1;

    [Header("Split")]
    [Tooltip("Enables the projectile to split off after impacting")]
    public bool SplitEnabled = true;
    [Tooltip("Chance of the projectile splitting after impacting")]
    [Range(0, 1)]
    public float SplitChance = 1f;
    [Tooltip("Minimum number of new projectiles")]
    public int SplitMinProjectiles = 2;
    [Tooltip("Maximum number of new projectiles")]
    public int SplitMaxProjectiles = 3;
    [Tooltip("Arc angle that indicates the spread of the new projectiles")]
    [Range(0, 180)]
    public float SplitArcAngle = 30f;
    [Tooltip("Size factor of the new projectiles")]
    public float SplitSizeMultiplier = 0.7f;

    private new Rigidbody rigidbody;
    private new Light light;
    private TrailRenderer trail;
    private new SphereCollider collider;
    private Vector3 startPosition;
    private int ricochets = 0;
    private int passthroughs = 0;

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        light = GetComponentInChildren<Light>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    void Start()
    {
        light.color = Color;

        trail.startColor = Color;
        trail.endColor = new Color(Color.r, Color.g, Color.b, 0);

        trail.startWidth = Size;
        trail.endWidth = 0;

        startPosition = transform.position;
    }

    void Update()
    {
        //rigidbody.velocity = new Vector3(Direction.x, 0, Direction.y) * Speed;
        rigidbody.velocity = Direction * Speed;

        if (SeekingEnabled)
        {
            UpdateSeeking();
        }

        if (Vector3.Distance(startPosition, transform.position) > Range)
        {
            Fizzle();
        }
    }

    void UpdateSeeking()
    {
        var closestEnemy = FindClosestEnemy(SeekingTargetDistance, SeekingTargetArcAngle);
        if (closestEnemy)
        {
            var enemyPosition = new Vector3(closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z);
            var enemyDirection = (enemyPosition - transform.position).normalized;

            Direction = Vector3.RotateTowards(Direction, enemyDirection, Mathf.Deg2Rad * SeekingAngularVelocity * Time.deltaTime, 0);
        }
    }

    GameObject FindClosestEnemy(float Radius, float ArcAngle)
    {
        GameObject closestEnemy = null;
        var closestEnemyDistance = Mathf.Infinity;

        var colliders = Physics.OverlapSphere(transform.position, Radius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                var enemy = collider.gameObject;
                var enemyPosition = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
                var enemyDistance = (enemyPosition - transform.position).sqrMagnitude;
                var enemyDirection = (enemyPosition - transform.position).normalized;

                if (Vector3.Angle(Direction, enemyDirection) > ArcAngle)
                {
                    continue;
                }

                if (enemyDistance < closestEnemyDistance)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = enemyDistance;
                }
            }
        }

        return closestEnemy;
    }

    private void Fizzle()
    {
        if (!GetComponent<DamageSource>().source.CompareTag(other.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    private void Impact()
    {
        CreateImpactEffect();

        // Destroy the projectile after some time has passed
        // so the trail renderer can continue
        rigidbody.detectCollisions = false;
        rigidbody.velocity = Vector3.zero;
        light.enabled = false;
        enabled = false;
        Destroy(gameObject, 1f);
    }
    
    private void CreateImpactEffect()
    {
        if (ImpactPrefab != null)
        {
            var impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileImpact>();
            impact.Color = Color;
        }
    }

    private IEnumerator TemporarilyIgnoreCollision(Collider other, float time)
    {
        Physics.IgnoreCollision(collider, other);
        yield return new WaitForSeconds(time);
        Physics.IgnoreCollision(collider, other, false);
    }

    public void IgnoreCollision(Collider other)
    {
        StartCoroutine(TemporarilyIgnoreCollision(other, IgnoreCollisionTime));
    }

    private void Split(Collider collider)
    {
        var projectiles = Random.Range(SplitMinProjectiles, SplitMaxProjectiles);

        for (int i = 0; i < projectiles; i++)
        {
            var projectile = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<Projectile>();

            projectile.IgnoreCollision(collider);

            var angle = i * (SplitArcAngle / (projectiles - 1)) - SplitArcAngle / 2;
            projectile.Direction = Quaternion.Euler(0, angle, 0) * Direction;

            projectile.Size = Size * SplitSizeMultiplier;

            // Really important! Or else the projectiles will grow exponentially
            projectile.SplitEnabled = false;
        }
    }

    private void Ricochet(Vector3 normal)
    {
        Direction = Vector3.Reflect(Direction, normal);
        Direction.y = 0;
        Direction.Normalize();

        // Move the projectile out of the wall a little
        //rigidbody.MovePosition(transform.position + Direction * 0.1f);
        transform.position += Direction * 0.2f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (PassthroughEnabled && Random.value < PassthroughChance && passthroughs++ < PassthroughMax)
            {
                CreateImpactEffect();
                IgnoreCollision(collision.collider);
            }
            else
            {
                if (SplitEnabled && Random.value < SplitChance)
                {
                    Split(collision.collider);
                }

                Impact();
            }
        }
        else if (collision.gameObject.CompareTag("Level"))
        {
            // TODO: Change to on collision stayed?
            if (RicochetEnabled && Random.value < RicochetChance && ricochets++ < RicochetMax)
            {
                var normal = collision.contacts[0].normal;
                Ricochet(normal);
            }
            else
            {
                Impact();
            }
        }
        else
        {
            Impact();
        }
    }
}
