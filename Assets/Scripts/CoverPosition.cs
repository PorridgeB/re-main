using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPosition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }
}
