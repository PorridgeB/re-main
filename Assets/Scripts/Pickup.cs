using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteract
{
    [SerializeField]
    private Resource targetResource;
    [SerializeField]
    private float value;
    [SerializeField]
    private AudioClip pickupSound;

    public void Interact()
    {
        SoundManager.PlaySound(pickupSound, 0.5f);
        targetResource.ChangeValue(value);
        Destroy(gameObject);
    }
}
