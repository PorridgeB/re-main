using BehaviorDesigner.Runtime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int unitCost;
    public EnemyType type;

    [Tooltip("The current group that the enemy belongs to. Maintained by the GroupManager")]
    public Group Group = null;

    [HideInInspector]
    public bool unstoppable = false;

    // Used by the Knockback action to know the direction of the hit
    public Vector3 hitDirection;

    public GameObject AttackField;
    public float AttackDamage = 10f;

    private const float SpriteFlipDeadzone = 0.2f;
    private const float AttackFieldDistance = 0.5f;

    [SerializeField]
    private GameEvent OnDeath;
    [SerializeField]
    private GameEvent OnHurt;
    [SerializeField]
    private GameObject damageToken;

    private Animator animator;
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private BehaviorTree behaviorTree;

    private float slowAmount;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        behaviorTree = GetComponent<BehaviorTree>();

        // Set the Player and PlayerTransform behaviour tree global variables
        GlobalVariables.Instance.SetVariableValue("Player", PlayerController.instance.gameObject);
        GlobalVariables.Instance.SetVariableValue("PlayerTransform", PlayerController.instance.transform);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Moving", agent.velocity.magnitude > 0.8f);

        if (Mathf.Abs(agent.velocity.x) > SpriteFlipDeadzone)
        {
            spriteRenderer.flipX = agent.velocity.x > 0;
        }
    }

    public void Slow(float duration)
    {
        if (slowAmount != 0) return;
        Debug.Log("Slowed");
        var bd = GetComponent<BehaviorTree>();
        float currentSpeed = (float)bd.GetVariable("Speed").GetValue();
        slowAmount = currentSpeed * 0.2f;
        bd.SetVariableValue("Speed", currentSpeed - slowAmount);
        StartCoroutine(StopSlow(duration));
    }

    private IEnumerator StopSlow(float duration)
    {
        yield return new WaitForSeconds(duration);
        var bd = GetComponent<BehaviorTree>();
        bd.SetVariableValue("Speed", (float)bd.GetVariable("Speed").GetValue() + slowAmount);
        slowAmount = 0;
    }

    public void Hurt(DamageInstance d)
    {
        // Stop it from hurting itself
        if (d.source != null)
        {
            if (CompareTag(d.source.tag))
            {
                return;
            }
        }
        

        GameObject g = Instantiate(damageToken, transform.position, new Quaternion());
        g.GetComponent<DamageToken>().SetValue(d);

        var health = behaviorTree.GetVariable("Health");
        behaviorTree.SetVariableValue("Health", (float)health.GetValue() - d.value);

        //OnHurt.Raise(gameObject);

        if ((float)behaviorTree.GetVariable("Health").GetValue() < 0f)
        {
            //OnDeath.Raise();

            if (!animator.parameters.Any(x => x.name == "Died"))
            {
                Destroy(gameObject);
            }

            behaviorTree.SendEvent("Die");
        }
    }

    // Called by Death animation event to destroy the enemy
    public void Die()
    {
        Destroy(gameObject);
    }

    public IEnumerator Attack()
    {
        var direction = (PlayerController.instance.transform.position - transform.position).normalized;

        var attackFieldInstance = Instantiate(AttackField, transform);

        DamageInstance damageInstance = new DamageInstance();
        damageInstance.type = DamageType.Physical;
        damageInstance.source = gameObject;
        damageInstance.value = AttackDamage;

        var damageSource = attackFieldInstance.GetComponent<DamageSource>();

        damageSource.source = gameObject;
        damageSource.AddInstance(damageInstance);

        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.z).normalized;

        attackFieldInstance.transform.localPosition = new Vector3(0, 0.5f, 0) + forwardDirection * AttackFieldDistance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        // Wait for a few frames
        yield return null;
        yield return null;

        Destroy(attackFieldInstance);
    }

    public void OnDamage(DamageSource source)
    {
        foreach (var effect in source.Effects)
        {
            effect.Resolve(gameObject);
        }

        foreach (var damage in source.Damages)
        {
            Hurt(damage);
        }

        // If the enemy is still alive
        var health = (float)behaviorTree.GetVariable("Health").GetValue();
        if (health > 0)
        {
            // Use the player's facing direction if the damage source was from the player.
            // Otherwise, calculate the hit direction based on the position of the damage source
            if (source.source.CompareTag("Player"))
            {
                var facing = PlayerController.instance.Facing;
                hitDirection = new Vector3(facing.x, 0, facing.y);
            }
            else
            {
                hitDirection = transform.position - source.transform.position;
                hitDirection.y = 0;
                hitDirection.Normalize();
            }

            // Tell the behaviour tree that the enemy has been hit
            if (!unstoppable) behaviorTree.SendEvent("Hit");
        }
    }

    public void DoAttack()
    {
        StartCoroutine(Attack());
    }
}
