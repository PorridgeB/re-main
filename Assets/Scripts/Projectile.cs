using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed;
    public float MaximumDistance = 25f;

    private new Rigidbody rigidbody;
    private Vector3 startPosition;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

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
