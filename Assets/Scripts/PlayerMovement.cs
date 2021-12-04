using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private CharacterController characterController;
    private Animator anim;
    private Vector2 velocity;

    public Vector2 GetVelocity()
    {
        return velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Moving", velocity != Vector2.zero);

        //rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime);

        characterController.Move(new Vector3(velocity.x, 0, velocity.y) * speed * Time.deltaTime);

        // Snap the player to the ground at all times.
        var playerPosition = characterController.transform.position;
        playerPosition.y = 1;
        characterController.transform.position = playerPosition;
    }

    private void FixedUpdate()
    {
    }

    public void SetVelocity(Vector2 vector)
    {
        velocity = vector;    
    }
}