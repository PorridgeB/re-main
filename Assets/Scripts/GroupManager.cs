using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroupManager : MonoBehaviour
{
    public float ProximityRadius = 5;
    public int MaxGroupSize = 5;
    public float UpdateRate = 1;

    [SerializeField]
    private List<Group> groups = new List<Group>();

    private void Start()
    {
        InvokeRepeating("UpdateGroups", 0, UpdateRate);
    }

    private void Update()
    {
    }

    private void UpdateGroups()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Enemy>());

        // Remove all groups
        groups.Clear();

        foreach (var enemy in enemies)
        {
            enemy.group = null;
        }

        foreach (var enemy in enemies)
        {
            // Find all the enemies that are within the proximity radius
            var enemiesInProximity = Physics.OverlapSphere(enemy.transform.position, ProximityRadius).Select(x => x.gameObject.GetComponent<Enemy>()).Where(x => x != null);

            // Find the first nearby group that isn't full
            var group = enemiesInProximity.Select(x => x.group).FirstOrDefault(x => x != null && x.Size < MaxGroupSize);

            // If no nearby groups are available to join, then create a new group
            if (group == null)
            {
                group = new Group();
                groups.Add(group);
            }

            // Add enemy to that group
            enemy.group = group;
            group.Add(enemy.gameObject);
        }

        // Remove all groups with only one member in them
        groups.RemoveAll(x => x.Size == 1);

        groups = groups.OrderByDescending(x => x.Size).ToList();
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var group in groups)
        {
            var positions = group.Select(x => x.transform.position).ToArray();
            var bounds = GeometryUtility.CalculateBounds(positions, Matrix4x4.identity);

            Handles.color = Color.Lerp(Color.blue, Color.red, group.Size / (float)MaxGroupSize);
            Handles.DrawWireDisc(bounds.center, Vector3.up, 0.5f + Mathf.Max(bounds.size.x, bounds.size.y));
        }
    }
}
