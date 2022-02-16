using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastUtils
{
    public static bool IsObstructed(Vector3 from, Vector3 to, float maxDistance)
    {
        from.y = 0;
        to.y = 0;

        var direction = (to - from).normalized;
        var distance = Vector3.Distance(from, to);

        return Physics.Raycast(from + Vector3.up, direction, Mathf.Min(maxDistance, distance), LayerMask.GetMask("Level"));
    }
}
