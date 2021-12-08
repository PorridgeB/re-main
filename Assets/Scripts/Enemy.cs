using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
            List<DamageInstance> source = collision.gameObject.GetComponent<DamageSource>().Damages;

            foreach (DamageInstance d in source)
            {
                // Stop it from hurting itself
                if (d.source != gameObject)
                {
                    var health = behaviorTree.GetVariable("Health");
                    behaviorTree.SetVariableValue("Health", (float)health.GetValue() - d.value);

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
    }
}
