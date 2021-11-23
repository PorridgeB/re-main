using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public delegate void Player();
    public static event Player Died;
    public static event Player PowerDepleted;

    private Animator anim;

    private float scrap = 0;
    private bool dashBlocked = true;
    private float viewDistance;
    private Vector2 facing;
    private Resource health;
    private ModuleInventory inventory;
    private Crosshair crosshair;
    //private Sword sword;
    //private Trail trail;
    [SerializeField]
    private Timer dashCooldown;
    [SerializeField]
    private Timer rangedCooldown;
    [SerializeField]
    private Timer meleeCooldown;

    private PlayerInput inputs;
    private PlayerMovement movement;
    private PlayerStats stats;

    public InputAction moveAction;
    public InputAction walkAction;
    public InputAction dashAction;
    public InputAction rangedAction;
    public InputAction meleeAction;



    public Vector2 GetFacing()
    {
        return facing;
    }



    // Start is called before the first frame update
    void Start()
    {
        

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            health = GetComponentInChildren<Resource>();
            crosshair = GetComponentInChildren<Crosshair>();
            anim = GetComponent<Animator>();

            inputs = GetComponent<PlayerInput>();
            movement = GetComponent<PlayerMovement>();
            stats = GetComponent<PlayerStats>();


            moveAction = inputs.actions["move"];
            walkAction = inputs.actions["walk"];
            dashAction = inputs.actions["Dash"];
            rangedAction = inputs.actions["RangedAttack"];
            meleeAction = inputs.actions["MeleeAttack"];

        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        facing = (crosshair.transform.position - transform.position).normalized;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Melee")) 
        {
            anim.SetFloat("Horizontal", facing.x);
            anim.SetFloat("Vertical", facing.y);
        }
        
        //As deadzones don't seem to work, I have added a manual deadzone so it will ignore input that is too small to be deliberate
        anim.SetFloat("VelX", Mathf.Abs(moveAction.ReadValue<Vector2>().x) > 0.2 ? moveAction.ReadValue<Vector2>().x : 0);
        anim.SetFloat("VelY", Mathf.Abs(moveAction.ReadValue<Vector2>().y) > 0.2 ? moveAction.ReadValue<Vector2>().y : 0);
        anim.SetBool("Sneak", walkAction.phase == InputActionPhase.Started);
        if (dashAction.phase == InputActionPhase.Started)
        {
            if (dashCooldown.Finished)
            {
                dashCooldown.Reset();
                anim.SetTrigger("Dash");
            }
        }
        if (meleeAction.phase == InputActionPhase.Started)
        {
            if (meleeCooldown.Finished)
            {
                meleeCooldown.Reset();
                anim.SetTrigger("Melee");
            }
        }
        if (rangedAction.phase == InputActionPhase.Started)
        {
            if (rangedCooldown.Finished)
            {
                rangedCooldown.Reset();
                anim.SetTrigger("Ranged");

            }
        }
            
    }

    public void Run()
    {
        movement.SetVelocity(moveAction.ReadValue<Vector2>().normalized * stats.ReadAttribute("Run Speed"));
    }

    public void Sneak()
    {
        movement.SetVelocity(moveAction.ReadValue<Vector2>().normalized * stats.ReadAttribute("Walk Speed"));
    }

    public void Dash(Vector2 velocity)
    {
        movement.SetVelocity(velocity);
    }

    public void Stop()
    {
        movement.SetVelocity(Vector2.zero);
    }

    public Vector2 GetVelocity()
    {
        return movement.GetVelocity();
    }
}
