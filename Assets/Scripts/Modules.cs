using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyModuleSlotPrefab;
    [SerializeField]
    private GameObject moduleSlotPrefab;
    [SerializeField]
    private GameObject moduleGrid;
    private List<Module> modules;

    private void GetModules()
    {
        foreach (Module m in Resources.LoadAll<Module>("Modules"))
        {
            modules.Add(m);
        }
    }

    private void OnEnable()
    {
        if (modules == null) modules = new List<Module>();
        if (modules.Count == 0) GetModules();
        Generate();
    }

    public void Generate()
    {
        for (int i = 0; i < modules.Count; i++)
        {
            if (modules[i].count != 0)
            {
                ModuleSlot moduleSlot = Instantiate(moduleSlotPrefab, moduleGrid.transform).GetComponent<ModuleSlot>();
                moduleSlot.SetUp(modules[i]);
            }
            
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
