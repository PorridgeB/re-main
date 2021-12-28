using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteract
{
    [SerializeField]
    private List<ModuleItem> moduleItems;

    public void Interact()
    {
        Instantiate(moduleItems[Random.Range(0, moduleItems.Count)], transform.position + Vector3.back, new Quaternion(), null);
        Destroy(GetComponentInChildren<Interaction>().gameObject);
    }
}
