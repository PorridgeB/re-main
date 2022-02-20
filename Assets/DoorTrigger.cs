using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private Animator animator;
    private List<Collider> colliders = new List<Collider>();

    private void UpdateState()
    {
        Debug.Log(string.Join(", ", colliders.Select(x => x.name)));
    }

    public void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);

        UpdateState();

        //if (!other.CompareTag("Player"))
        //{
        //    return;
        //}

        //animator.SetBool("Opened", true);
        //rigidbody.detectCollisions = false;
        //Debug.Log("Disabling collisions");
    }

    public void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);

        UpdateState();

        //if (!other.CompareTag("Player"))
        //{
        //    return;
        //}

        //animator.SetBool("Opened", false);
        //rigidbody.detectCollisions = true;
        //Debug.Log("Enabling collisions");
    }
}
