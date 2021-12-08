using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [SerializeField]
    private GameEvent OnDeath;
    [SerializeField]
    private GameEvent OnHurt;

    private float slowAmount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DamageSource"))
        {
            List<DamageInstance> source = collision.gameObject.GetComponent<DamageSource>().Damages;

            foreach (DamageInstance d in source)
            {
                Debug.Log("hurting for " + d.value);
                Hurt(d);
            }
            

            
        }
    }

    public void Slow()
    {
        if (slowAmount != 0) return;
        Debug.Log("Slowed");
        var bd = GetComponent<BehaviorTree>();
        float currentSpeed = (float)bd.GetVariable("Speed").GetValue();
        slowAmount = currentSpeed * 0.2f;
        bd.SetVariableValue("Speed", currentSpeed - slowAmount);
        StartCoroutine(StopSlow());
    }

    private IEnumerator StopSlow()
    {
        yield return new WaitForSeconds(2);
        var bd = GetComponent<BehaviorTree>();
        bd.SetVariableValue("Speed", (float)bd.GetVariable("Speed").GetValue() + slowAmount);
        slowAmount = 0;
    }

    public void Hurt(DamageInstance d)
    {
        // Stop it from hurting itself
        if (d.source != gameObject)
        {
            Debug.Log(d.value);
            var bd = GetComponent<BehaviorTree>();
            var health = bd.GetVariable("Health");
            bd.SetVariableValue("Health", (float)health.GetValue() - d.value);

            OnHurt.Raise(gameObject);

            if ((float)bd.GetVariable("Health").GetValue() < 0f)
            {
                OnDeath.Raise();
                Destroy(gameObject);
            }
        }
    }
}
