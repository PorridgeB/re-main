using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private GameEvent OnDeath;
    [SerializeField]
    private GameEvent OnHurt;
    [SerializeField]
    private GameObject damageToken;


    private float slowAmount;
    private BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DamageSource"))
        {
            DamageSource source = collision.gameObject.GetComponent<DamageSource>();
            foreach (Effect e in source.Effects)
            {
                e.Resolve(gameObject);
            }
            foreach (DamageInstance d in source.Damages)
            {
                Hurt(d);
                // Stop it from hurting itself
               
            }
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

        if (d.source != gameObject)
        {
            GameObject g = Instantiate(damageToken, transform.position, new Quaternion());
            g.GetComponent<DamageToken>().SetValue(d);

            var health = behaviorTree.GetVariable("Health");
            behaviorTree.SetVariableValue("Health", (float)health.GetValue() - d.value);

            OnHurt.Raise(gameObject);

            if ((float)behaviorTree.GetVariable("Health").GetValue() < 0f)
            {
                Destroy(gameObject);

                behaviorTree.SendEvent("Died");
            }
            else
            {
                behaviorTree.SendEvent("Hit");
            }
        }
    }
}
