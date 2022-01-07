using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    private BehaviorTree behaviorTree;
    private Memory memory;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        memory = GetComponent<Memory>();
    }

    private void Update()
    {
        behaviorTree.SetVariableValue("Target", PlayerController.instance.gameObject);
    }
}
