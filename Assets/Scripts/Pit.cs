using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var dashableLayer = LayerMask.NameToLayer("Dashable");
            var playerLayer = LayerMask.NameToLayer("Player");

            Physics.IgnoreLayerCollision(playerLayer, dashableLayer, false);
            Debug.Log("collisions on");
        }
    }
}
