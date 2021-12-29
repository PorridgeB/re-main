using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Direction of travel (normalized)
    public Vector3 Direction;
    // Speed of the projectile (in unit/s)
    public float Speed = 10f;
    // Maximum distance the projectile can travel from its starting position before being destroyed
    public float Range = 25f;
    // Number of enemy collisions before the projectile is destroyed
    public int MaximumHits = 1;
    // Radius of the sphere collider and the width of the trail
    public float Size = 0.25f;
    // Colour of the light and the trail
    public Color Color = Color.white;
    [Header("Seeking")]
    // Enables the projectile to bend toward nearby enemies
    public bool SeekingEnable = true;
    // Maximum angle the projectile is allowed to turn within a second when seeking an enemy (in deg/s)
    public float SeekingAngularVelocity = 70f;
    // Maximum distance that the projectile searches for nearby enemies
    public float SeekingTargetDistance = 5f;
    // Maximum arc angle between the projectile's direction of travel and direction to enemy for seeking
    public float SeekingTargetArcAngle = 60f;

    private new Rigidbody rigidbody;
    private Vector3 startPosition;
    private HashSet<int> hits = new HashSet<int>();

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        var light = GetComponentInChildren<Light>();
        var trailRenderer = GetComponentInChildren<TrailRenderer>();
        var sphereCollider = GetComponent<SphereCollider>();

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
        //var velocity = new Vector3(Direction.x, 0, Direction.y) * Speed;
        var velocity = Direction * Speed;
        rigidbody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (SeekingEnable)
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

    void Fizzle()
    {
        Destroy(gameObject);
    }

    void Impact()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var instanceId = collision.gameObject.GetInstanceID();

            if (!hits.Contains(instanceId))
            {
                hits.Add(instanceId);

                if (hits.Count >= MaximumHits)
                {
                    Impact();
                }
            }
        }
        else if (collision.gameObject.CompareTag("Level"))
        {
            Impact();
        }
        else
        {
            Impact();
        }
    }
}
