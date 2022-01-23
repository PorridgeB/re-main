using System.Linq;
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
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Enemy>()).Where(x => ShowHealthBar(x));

        foreach (var enemy in enemies)
        {
            var healthBar = Instantiate(HealthBarPrefab, transform).GetComponent<HealthBar>();

            healthBar.Object = enemy.gameObject;
            healthBar.Camera = Camera;
        }
    }

    private void FixedUpdate()
    {
    }

    private bool ShowHealthBar(Enemy enemy)
    {
        return true;
    }
}
