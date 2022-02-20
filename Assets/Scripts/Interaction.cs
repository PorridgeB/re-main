using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onInteract;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeVisibility(false);
    }

    public void ChangeVisibility(bool value)
    {
        spriteRenderer.enabled = value;
    }

    public void Interact()
    {
        //transform.parent.GetComponent<IInteract>().Interact();

        onInteract.Invoke();
    }
}
