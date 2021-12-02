using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public Vector2 Velocity;

    private CharacterController characterController;
    private Animator animator;

    public void SetVelocity(Vector2 velocity)
    {
        Velocity = velocity;
    }

    public Vector2 GetVelocity()
    {
        return Velocity;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("Moving", Velocity != Vector2.zero);
        characterController.Move(new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime);

        // Snap the player to the ground at all times.
        var playerPosition = characterController.transform.position;
        playerPosition.y = 1;
        characterController.transform.position = playerPosition;
    }
}
