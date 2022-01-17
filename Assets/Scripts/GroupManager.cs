using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroupManager : MonoBehaviour
{
    public float ProximityRadius = 3;
    public int MaxGroupSize = 5;

    [Serializable]
    public class Group
    {
        public List<GameObject> Members = new List<GameObject>();

        public int Size => Members.Count;
    }

    [SerializeField]
    private List<Group> groups = new List<Group>();

    private void Start()
    {
        InvokeRepeating("UpdateGroups", 0, 0.5f);
    }

    private void Update()
    {
    }

    private void UpdateGroups()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Enemy>());

        groups.Clear();

        foreach (var enemy in enemies)
        {
            enemy.group = -1;
        }

        foreach (var enemy in enemies)
        {
            var proximityEnemies = Physics.OverlapSphere(enemy.transform.position, ProximityRadius).Select(x => x.gameObject.GetComponent<Enemy>()).Where(x => x != null);

            int group = -1;

            foreach (var proximityEnemy in proximityEnemies)
            {
                if (proximityEnemy.group != -1)
                {
                    if (groups[proximityEnemy.group].Size < MaxGroupSize)
                    {
                        group = proximityEnemy.group;
                    }
                }
            }

            if (group == -1)
            {
                group = groups.Count;
                groups.Add(new Group());
            }

            enemy.group = group;
            groups[group].Members.Add(enemy.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
    }
}
