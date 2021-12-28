using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed = 10f;
    public float Range = 25f;
    public float Size = 0.25f;
    public Color Color = Color.white;
    [Header("Seeking")]
    public bool SeekingEnable = true;
    public float SeekingAngularVelocity = 70f; // Degrees per second
    public float SeekingTargetDistance = 5f;

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
            Impact();
        }
    }

    void UpdateSeeking()
    {
        var closestEnemy = FindClosestEnemy(SeekingTargetDistance);
        if (closestEnemy)
        {
            var enemyPosition = new Vector3(closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z);
            var enemyDirection = (enemyPosition - transform.position).normalized;

            Direction = Vector3.RotateTowards(Direction, enemyDirection, Mathf.Deg2Rad * SeekingAngularVelocity * Time.deltaTime, 0);
        }
    }

    GameObject FindClosestEnemy(float Radius)
    {
        GameObject closestEnemy = null;
        var closestEnemyDistance = Mathf.Infinity;

        var colliders = Physics.OverlapSphere(transform.position, Radius);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                var enemy = collider.gameObject;
                var enemyPosition = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
                var enemyDistance = (enemyPosition - transform.position).sqrMagnitude;

                if (enemyDistance < closestEnemyDistance)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = enemyDistance;
                }
            }
        }

        return closestEnemy;
    }

    void Impact()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Impact();
    }
}
