using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    private float speed;
    private Vector3 startPosition;

    private bool shot;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > 50)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 position, Vector2 direction, float velocity)
    {
        shot = true;
        transform.position = position;
        dir = direction;
        speed = velocity;
        GetComponent<Rigidbody>().velocity = velocity * new Vector3(direction.x, 0, direction.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<DamageSource>().source.CompareTag(other.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
