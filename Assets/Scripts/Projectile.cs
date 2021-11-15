using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * dir.x, speed*dir.y);
    }

    public void Shoot(Vector2 position, Vector2 direction, float velocity)
    {
        transform.position = position;
        dir = direction;
        speed = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
