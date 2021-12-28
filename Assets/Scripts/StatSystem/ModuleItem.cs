using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleItem : MonoBehaviour, IInteract
{
    [SerializeField]
    private Module module;
    [SerializeField]
    private GameEvent modulePickup;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = module.sprite;
    }

    public void Interact()
    {
        module.count++;
        foreach (Bonus b in module.bonuses)
        {
            b.attribute.AddModuleBonus(b);
        }
        modulePickup.Raise();
        Destroy(gameObject);
        
    }
}
