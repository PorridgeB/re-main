using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleItem : MonoBehaviour
{
    [SerializeField]
    private Module module;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = module.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ModuleInventory>().AddModule(module);
        Destroy(gameObject);
    }

    public Module Module
    {
        get
        {
            return module;
        }
    }
}
