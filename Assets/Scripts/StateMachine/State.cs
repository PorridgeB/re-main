using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class State : MonoBehaviour
{
    protected StateMachine stateMachine;
    public string name;

    protected PlayerInput inputs;
    protected PlayerMovement movement;
    protected PlayerStats stats;

    protected InputAction moveAction;
    protected InputAction walkAction;

    private void Start()
    {

        movement = GetComponentInParent<PlayerMovement>();
        inputs = GetComponentInParent<PlayerInput>();
        stats = GetComponentInParent<PlayerStats>();

        moveAction = inputs.actions["move"];
        walkAction = inputs.actions["walk"];
    }
    public void SetStateMachine(StateMachine machine)
    {
        stateMachine = machine;
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void Process()
    {
        
    }

    public virtual void PhysicsProcess()
    {

    }

    public virtual void Enter(List<string> message)
    {

    }

    public virtual void Exit()
    {

    }
}
