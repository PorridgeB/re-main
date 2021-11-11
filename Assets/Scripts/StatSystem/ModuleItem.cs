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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ModuleInventory>().AddModule(module);
        Destroy(gameObject);
    }
}
