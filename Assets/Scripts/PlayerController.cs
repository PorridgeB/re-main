using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameEvent playerHit;

    [SerializeField]
    private AttackEvent nextAttack;
    private Animator anim;

    public readonly List<DamageInstance> damageTaken = new List<DamageInstance>();

    private float scrap = 0;
    private bool dashBlocked = true;
    private float viewDistance;
    private Vector2 facing;
    private float health = 100f;
    //private Crosshair crosshair;

    [SerializeField]
    private List<Dash> dashes;
    [SerializeField]
    private GameObject dash;
    [SerializeField]
    private Timer rangedCooldown;
    [SerializeField]
    private Timer meleeCooldown;
    [SerializeField]
    private Timer healthRegenTimer;

    [SerializeField]
    private List<Interaction> interactions;
    private Interaction selectedInteraction;

    private PlayerInput inputs;
    private PlayerMovement movement;
    [SerializeField]
    private StatBlock stats;

    private InputAction moveAction;
    private InputAction walkAction;
    private InputAction dashAction;
    private InputAction rangedAction;
    private InputAction meleeAction;
    private InputAction interactAction;
    private InputAction overlayAction;

    public Vector2 GetFacing()
    {
        return facing;
    }



    // Start is called before the first frame update
    void Awake()
    {
        

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        //health = GetComponentInChildren<Resource>();
        //crosshair = GetComponentInChildren<Crosshair>();
        anim = GetComponent<Animator>();

        inputs = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();


        moveAction = inputs.actions["move"];
        walkAction = inputs.actions["walk"];
        dashAction = inputs.actions["Dash"];
        rangedAction = inputs.actions["RangedAttack"];
        meleeAction = inputs.actions["MeleeAttack"];
        interactAction = inputs.actions["Interact"];
        overlayAction = inputs.actions["Overlay"];

        for (int i = 0; i < stats.DashCharges.Value(); i++)
        {
            GameObject g = Instantiate(dash);
            g.transform.SetParent(transform);
            dashes.Add(g.GetComponent<Dash>());
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (interactAction.triggered)
        {
            selectedInteraction?.Interact();
        }
        if (overlayAction.triggered)
        {
            inputs.SwitchCurrentActionMap("OverlayControl");
        }

        //facing = (crosshair.transform.position - transform.position).normalized;
        facing = (Mouse.current.position.ReadValue() - new Vector2(Screen.width, Screen.height) / 2).normalized;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Melee")) 
        {
            anim.SetFloat("Horizontal", facing.x);
            anim.SetFloat("Vertical", facing.y);
        }
        
        //As deadzones don't seem to work, I have added a manual deadzone so it will ignore input that is too small to be deliberate
        anim.SetFloat("VelX", Mathf.Abs(moveAction.ReadValue<Vector2>().x) > 0.2 ? moveAction.ReadValue<Vector2>().x : 0);
        anim.SetFloat("VelY", Mathf.Abs(moveAction.ReadValue<Vector2>().y) > 0.2 ? moveAction.ReadValue<Vector2>().y : 0);
        anim.SetBool("Sneak", walkAction.phase == InputActionPhase.Started);
        if (dashAction.triggered)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {
                ActivateDash();
            }
            
        }
        if (meleeAction.phase == InputActionPhase.Started)
        {
            if (meleeCooldown.Finished)
            {
                //Attack Speed represents the amount of attacks per second. Cooldown is therefore 1/attacks per second
                meleeCooldown.Reset(1 / stats.MeleeAttackSpeed.Value());
                anim.SetTrigger("Melee");
            }
        }
        if (rangedAction.phase == InputActionPhase.Started)
        {
            if (rangedCooldown.Finished)
            {
                //Attack Speed represents the amount of attacks per second. Cooldown is therefore 1/attacks per second
                rangedCooldown.Reset(1/stats.RangedAttackSpeed.Value());
                anim.SetTrigger("Ranged");

            }
        }
        if (interactions.Count == 1)
        {
            selectedInteraction = interactions[0];
        }
        else
        {
            foreach (Interaction i in interactions)
            {
                if (i != null)
                {
                    i.ChangeVisibility(false);
                    if (selectedInteraction == null)
                    {
                        selectedInteraction = i;
                    }
                    else if (Vector2.Distance(transform.position, i.transform.position) < Vector2.Distance(transform.position, selectedInteraction.transform.position))
                    {
                        selectedInteraction = i;
                    }
                }
                
            }
        }
        
        if (selectedInteraction != null)
        {
            selectedInteraction.ChangeVisibility(true);
        }
        if (healthRegenTimer.Finished)
        {
            health += 1;
            healthRegenTimer.Reset(1/stats.HealthRegen.Value());
        }
    }

    public void ActivateDash()
    {
        foreach (Dash d in dashes)
        {
            if (d.Ready)
            {
                d.Activate(1 / stats.DashRechargeRate.Value());
                anim.SetTrigger("Dash");
                return;
            }
        }
    }

    public void Run()
    {
        movement.SetVelocity(moveAction.ReadValue<Vector2>().normalized * stats.RunSpeed.Value());
    }

    public void Sneak()
    {
        movement.SetVelocity(moveAction.ReadValue<Vector2>().normalized * stats.WalkSpeed.Value());
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

    public void StartDialogue()
    {
        inputs.SwitchCurrentActionMap("DialogueControl");
    }

    public void OpenArtifact()
    {
        inputs.SwitchCurrentActionMap("ArtifactControl");
    }

    public void ReceiveDamage(DamageInstance damage)
    {
        if (CheckDodge())
        {
            Debug.Log("Dodged");

            //Do dodge logic here
            return;
        }

        damageTaken.Insert(0, damage);
        //playerHit.Raise();

        float finalDamageValue = 0;
        switch (damage.type)
        {
            case DamageType.Elemental:
                finalDamageValue = damage.value *1-stats.ResistanceElemental.Value();
                break;
            case DamageType.Energy:
                finalDamageValue = damage.value * 1 - stats.ResistanceEnergy.Value();
                break;
            case DamageType.Physical:
                finalDamageValue = damage.value * 1 - stats.ResistancePhysical.Value();
                break;
        }
        health -= finalDamageValue;
    }

    private bool CheckDodge()
    {
        return Random.value < stats.DodgeChance.Value();
    }

    private bool CheckCrit()
    {
        return Random.value < stats.CritChance.Value();
    }

    public void AddDamage(DamageSource source)
    {
        if (source.name == "Projectile")
        {
            source.AddInstance(GetRangedDamage());
        }
        else
        {
            source.AddInstance(GetMeleeDamage());
        }
    }
    
    private DamageInstance GetRangedDamage()
    {
        DamageInstance d = new DamageInstance();
        d.value = stats.RangedAttackDamage.Value();
        d.source = gameObject;
        d.crit = CheckCrit();
        if (d.crit)
        {
            d.value = GetCrit(d.value);
        }
        d.type = DamageType.Physical;
        return d;
    }

    private DamageInstance GetMeleeDamage()
    {
        DamageInstance d = new DamageInstance();
        d.value = stats.MeleeAttackDamage.Value();
        d.source = gameObject;
        d.crit = CheckCrit();
        if (d.crit)
        {
            d.value = GetCrit(d.value);
        }
        d.type = DamageType.Physical;
        return d;
    }

    public float GetCrit(float value)
    {
        return value * stats.CritDamage.Value();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            interactions.Add(collision.GetComponent<Interaction>());
        }
        else if (collision.CompareTag("DamageSource"))
        {
            DamageSource d = collision.gameObject.GetComponent<DamageSource>();
            foreach (DamageInstance damage in d.Damages)
            {
                // Stop the player from hurting itself
                if (damage.source != gameObject)
                {
                    ReceiveDamage(damage);
                    //Debug.Log(health);
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            Interaction i = collision.GetComponent<Interaction>();
            i.ChangeVisibility(false);

            interactions.Remove(collision.GetComponent<Interaction>());
            if (selectedInteraction == i)
            {
                selectedInteraction = null;
            }
        }
    }
}
