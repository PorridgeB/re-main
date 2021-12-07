using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [SerializeField]
    private GameEvent OnDeath;
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
                // Stop it from hurting itself
                if (d.source != gameObject)
                {
                    var bd = GetComponent<BehaviorTree>();
                    var health = bd.GetVariable("Health");
                    bd.SetVariableValue("Health", (float)health.GetValue() - d.value);


                    if ((float)bd.GetVariable("Health").GetValue() < 0f)
                    {
                        OnDeath.Raise();
                        Destroy(gameObject);
                    }
                }
            }
            

            
        }
    }
}
