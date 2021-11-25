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

    public Module Module
    {
        get
        {
            return module;
        }
    }
}
