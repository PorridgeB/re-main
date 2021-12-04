using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
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
            DamageInstance source = collision.gameObject.GetComponent<DamageSource>().Damage;

            // Stop it from hurting itself
            if (source.source != gameObject)
            {
                var bd = GetComponent<BehaviorTree>();
                var health = bd.GetVariable("Health");
                bd.SetVariableValue("Health", (float)health.GetValue() - source.value);


                if ((float)bd.GetVariable("Health").GetValue() < 0f)
                {
                    Destroy(gameObject);
                }
            }

            
        }
    }
}
