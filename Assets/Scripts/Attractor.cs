using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 4;
    [SerializeField]
    private float minSpeed = 2;
    [SerializeField]
    private float maxSpeed = 25;
    [SerializeField]
    private List<string> tags = new List<string> { "Player" };

    private void FixedUpdate()
    {
        var attractor = GetAttractor();

        if (attractor != null)
        {
            var positionDelta = attractor.transform.position - transform.position;
            positionDelta.y = 0;

            var distance = positionDelta.magnitude;
            var direction = positionDelta.normalized;

            var speed = Mathf.Lerp(minSpeed, maxSpeed, 1 - distance / maxDistance);

            transform.position += direction * speed * Time.fixedDeltaTime;
        }
    }

    private GameObject GetAttractor()
    {
        var attractors = tags.SelectMany(x => GameObject.FindGameObjectsWithTag(x));

        foreach (var attractor in attractors)
        {
            var to = attractor.transform.position;
            var from = transform.position;

            to.y = 0;
            from.y = 0;

            var direction = (to - from).normalized;
            var distance = Vector3.Distance(from, to);

            if (distance > maxDistance)
            {
                continue;
            }

            if (!Physics.Raycast(from + Vector3.up, direction, Mathf.Min(maxDistance, distance), LayerMask.GetMask("Level")))
            {
                return attractor.gameObject;
            }
        }

        return null;
    }
}
