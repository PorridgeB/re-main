using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Moving", velocity != Vector2.zero);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime);
    }

    public void SetVelocity(Vector2 vector)
    {
        velocity = vector;    }
}