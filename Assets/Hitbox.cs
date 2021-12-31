using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DamageSource"))
        {
            var source = other.gameObject.GetComponent<DamageSource>();
            SendMessageUpwards("OnDamage", source);
        }
    }
}
