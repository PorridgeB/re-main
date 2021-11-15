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
    protected PlayerController controller;

    protected InputAction moveAction;
    protected InputAction walkAction;
    protected InputAction dashAction;
    protected InputAction rangedAction;
    protected InputAction meleeAction;


    private void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        inputs = GetComponentInParent<PlayerInput>();
        stats = GetComponentInParent<PlayerStats>();
        controller = GetComponentInParent<PlayerController>();

        moveAction = inputs.actions["move"];
        walkAction = inputs.actions["walk"];
        dashAction = inputs.actions["Dash"];
        rangedAction = inputs.actions["RangedAttack"];
        meleeAction = inputs.actions["MeleeAttack"];
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
