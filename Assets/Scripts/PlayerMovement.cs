using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20;
    public float jumpForce = 8;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //At least one parameter will be above 0, giving the idle animation a direction to face that the player was last moving
        if (input.x < -0.05 || input.x > 0.05 || input.y < -0.05 || input.y > 0.05)
        {
            anim.SetFloat("Horizontal", input.x);
            anim.SetFloat("Vertical", input.y);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }
}