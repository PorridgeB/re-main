using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteract
{
    [SerializeField]
    private List<ModuleItem> moduleItems;
    [SerializeField]
    private AudioClip impact;
    [SerializeField]
    private AudioClip fanfare;

    public void Interact()
    {
        SoundManager.PlaySound(impact, 0.2f);
        SoundManager.PlaySound(fanfare, 0.3f);
        Instantiate(moduleItems[Random.Range(0, moduleItems.Count)], transform.position + Vector3.back, new Quaternion(), null);
        Destroy(GetComponentInChildren<Interaction>().gameObject);
    }
}
