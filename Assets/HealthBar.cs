using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Camera Camera;
    public GameObject Object;
    //public float HealthPercentage = 1;

    [SerializeField]
    private RectTransform Foreground;

    private void Update()
    {
        if (Object == null)
        {
            Destroy(gameObject);
        }

        var screenPoint = Camera.WorldToScreenPoint(Object.transform.position + new Vector3(0, 1.5f, 0));

        var rectTrans = GetComponent<RectTransform>();
        rectTrans.anchoredPosition = screenPoint;

        var behaviorTree = Object.GetComponent<BehaviorTree>();

        var health = (float)behaviorTree.GetVariable("Health").GetValue();
        var maxHealth = (float)behaviorTree.GetVariable("MaxHealth").GetValue();

        var healthPercentage = health / maxHealth;

        Foreground.localScale = new Vector3(healthPercentage, 1, 1);
    }
}
