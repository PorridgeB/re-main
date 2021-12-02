using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    private float speed;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(speed * dir.x, 0, speed * dir.y);
        transform.position += move;
        
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
        transform.position = position;
        dir = direction;
        speed = velocity;
    }
}
