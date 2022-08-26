using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup2 : MonoBehaviour, IInteract
{
    public GameEvent pickup;
    [SerializeField]
    private AudioClip pickupSound;

    public void Interact()
    {
        pickup.Raise();
        SoundManager.PlaySound(pickupSound, 0.5f);
        Destroy(gameObject);
    }
}
