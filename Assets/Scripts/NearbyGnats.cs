using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyGnats : MonoBehaviour
{
    public float Radius = 6f;

    private BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();

        InvokeRepeating("CalculateNearbyGnats", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CalculateNearbyGnats()
    {
        int count = 0;

        var hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<NearbyGnats>() != null)
            {
                count++;
            }
        }

        behaviorTree.SetVariableValue("NearbyGnats", count);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
