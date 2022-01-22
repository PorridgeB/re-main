using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBars : MonoBehaviour
{
    public Camera Camera;
    public GameObject HealthBarPrefab;

    private void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            var healthBar = Instantiate(HealthBarPrefab, transform).GetComponent<HealthBar>();

            healthBar.Object = enemy;
            healthBar.Camera = Camera;
        }
    }

    private void Update()
    {
    }
}
