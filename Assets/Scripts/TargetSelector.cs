using System.Linq;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TargetSelector : MonoBehaviour
{
    [Tooltip("Objects with this tag will be candidates for targetting")]
    public string Tag = "Player";
    [Tooltip("How much to favour targets that are close")]
    [Range(0, 1)]
    public float DistanceFactor = 0.5f;
    [Tooltip("How much to favour targets with low health")]
    [Range(0, 1)]
    public float HealthFactor = 0.7f;

    private BehaviorTree behaviorTree;
    private Memory memory;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        memory = GetComponent<Memory>();
    }

    private void Update()
    {
        var target = memory.WithTag(Tag).Select(x => x.Who).OrderByDescending(x => EvaluateTarget(x)).FirstOrDefault();
        behaviorTree.SetVariableValue("Target", target);
    }

    private float EvaluateTarget(GameObject target)
    {
        var distance = Vector3.Distance(transform.position, target.transform.position) / 5;

        // Try and get the health value
        float health;

        var player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            health = player.Health / 100;
        }
        else
        {
            var behaviorTree = target.GetComponent<BehaviorTree>();
            health = (float)behaviorTree?.GetVariable("Health")?.GetValue() / 100;
        }

        return DistanceFactor / distance + HealthFactor / health;
    }

    private void OnDrawGizmosSelected()
    {
        var target = behaviorTree?.GetVariable("Target").GetValue() as GameObject;

        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(target.transform.position + Vector3.up, new Vector3(1, 2, 1));

            //Handles.color = Color.red;
            //Handles.DrawWireDisc(target.transform.position, Vector3.up, 0.5f);

            //Handles.Label(target.transform.position, $"Target Eval: {EvaluateTarget(target)}");
        }
    }
}