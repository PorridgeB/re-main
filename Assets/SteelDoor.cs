using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelDoor : MonoBehaviour
{
    public bool Closed
    {
        get => animator.GetBool("Closed");
        set
        {
            animator.SetBool("Closed", value);
            rigidbody.detectCollisions = value;
            audioSource.PlayOneShot(value ? close : open);
        }
    }

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip open;
    [SerializeField]
    private AudioClip close;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Open()
    {
        Closed = false;
    }

    public void Close()
    {
        Closed = true;
    }

    public void Toggle()
    {
        Closed = !Closed;
    }
}
