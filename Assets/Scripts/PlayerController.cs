using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Vector2 Facing => facing;

    public GameEvent playerHit;
    [SerializeField]
    public GameEvent playerDeath;

    private Animator anim;
    private AttackEvent nextAttack;
    private Animator animator;

    public readonly List<DamageInstance> damageTaken = new List<DamageInstance>();

    private float scrap = 0;
    private bool dashBlocked = true;
    private float viewDistance;
    private Vector2 facing;
    // temporary (use SO)
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
    private Timer specialRangedCooldown;
    [SerializeField]
    private Timer specialMeleeCooldown;
    [SerializeField]
    private Timer healthRegenTimer;

    [SerializeField]
    private List<Interaction> interactions;
    private Interaction selectedInteraction;

    private PlayerInput inputs;
    private PlayerMovement movement;
    [SerializeField]
    private StatBlock stats;

    [SerializeField]
    private GameObject damageToken;

    private InputAction moveAction;
    private InputAction walkAction;
    private InputAction dashAction;
    private InputAction rangedAction;
    private InputAction meleeAction;
    private InputAction interactAction;
    private InputAction overlayAction;

    // Temporary way to switch weapons in-game for debug purposes
    private List<Weapon> meleeWeapons = new List<Weapon>() { new Sword(), new Hammer() };
    private List<Weapon> rangedWeapons = new List<Weapon>() { new Phaser(), new Railgun() };

    private Weapon meleeWeapon;
    private Weapon rangedWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
        }
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

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

        foreach (var weapon in meleeWeapons) weapon.Animator = animator;
        foreach (var weapon in rangedWeapons) weapon.Animator = animator;

        meleeWeapon = meleeWeapons[0];
        rangedWeapon = rangedWeapons[0];

        inputs.actions["RangedAttack"].canceled += OnRangedAttackCanceled;
    }

    private void OnRangedAttackCanceled(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("RangedRelease");
    }

    // Update is called once per frame
    void Update()
    {
        interactions.RemoveAll(delegate (Interaction i) { return i == null; });

        facing = (Mouse.current.position.ReadValue() - new Vector2(Screen.width, Screen.height) / 2).normalized;
        
        if (interactAction.triggered)
        {
            selectedInteraction?.Interact();
        }

        if (overlayAction.triggered)
        {
            inputs.SwitchCurrentActionMap("OverlayControl");
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Melee")) 
        {
            animator.SetFloat("Horizontal", facing.x);
            animator.SetFloat("Vertical", facing.y);
        }
        
        // As deadzones don't seem to work, I have added a manual deadzone so it will ignore input that is too small to be deliberate
        animator.SetFloat("VelX", Mathf.Abs(moveAction.ReadValue<Vector2>().x) > 0.2 ? moveAction.ReadValue<Vector2>().x : 0);
        animator.SetFloat("VelY", Mathf.Abs(moveAction.ReadValue<Vector2>().y) > 0.2 ? moveAction.ReadValue<Vector2>().y : 0);
        animator.SetBool("Sneak", walkAction.phase == InputActionPhase.Started);

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
            //health.ChangeValue(1);
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
                animator.SetTrigger("Dash");
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
        playerHit.Raise();

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
        //health.ChangeValue(-finalDamageValue);
        health -= finalDamageValue;
        CreateDamageToken(finalDamageValue);
        //if (health.Value < 0)
        if (health < 0)
        {
            playerDeath.Raise();
        }
    }

    private void CreateDamageToken(float value)
    {
        GameObject g = Instantiate(damageToken, transform.position, new Quaternion());
        g.GetComponent<DamageToken>().SetValue(value);
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
        if (source.name == "Projectile(Clone)")
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

    public void OnRangedAttack()
    {
        if (rangedCooldown.Finished)
        {
            // Attack Speed represents the amount of attacks per second. Cooldown is therefore 1/attacks per second
            rangedCooldown.Reset(1 / stats.RangedAttackSpeed.Value());
            rangedWeapon?.Fire();
        }
    }

    public void OnRangedSpecialAttack()
    {
        if (specialRangedCooldown.Finished)
        {
            // Attack Speed represents the amount of attacks per second. Cooldown is therefore 1/attacks per second
            specialRangedCooldown.Reset(1 / stats.SpecialRangedAttackSpeed.Value());
            rangedWeapon?.SpecialFire();
        }
    }

    public void OnMeleeAttack()
    {
        if (meleeCooldown.Finished)
        {
            // Attack Speed represents the amount of attacks per second. Cooldown is therefore 1/attacks per second
            meleeCooldown.Reset(1 / stats.MeleeAttackSpeed.Value());
            meleeWeapon?.Fire();
        }
    }

    public void OnMeleeSpecialAttack()
    {
        if (specialMeleeCooldown.Finished)
        {
            // Attack Speed represents the amount of attacks per second. Cooldown is therefore 1/attacks per second
            specialMeleeCooldown.Reset(1 / stats.SpecialMeleeAttackSpeed.Value());
            meleeWeapon?.SpecialFire();
        }
    }

    public void OnDash()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            ActivateDash();
        }
    }

    private Weapon NextWeapon(Weapon currentWeapon, List<Weapon> weapons)
    {
        int currentWeaponIndex = weapons.FindIndex(x => x == currentWeapon);
        int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        return weapons[nextWeaponIndex];
    }

    public void OnNextMeleeWeapon()
    {
        meleeWeapon = NextWeapon(meleeWeapon, meleeWeapons);
    }

    public void OnNextRangedWeapon()
    {
        rangedWeapon = NextWeapon(rangedWeapon, rangedWeapons);
    }
    
    public void OnDamage(DamageSource source)
    {
        foreach (var instance in source.Damages)
        {
            // Stop the player from hurting itself
            if (instance.source != gameObject)
            {
                ReceiveDamage(instance);
                Debug.Log(health);
            }
        }
    }
}
