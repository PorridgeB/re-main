using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ModuleInventory : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;
    [SerializeField]
    private List<Module> modules;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAttributes()
    {
        foreach (Module m in modules) {
            foreach (Bonus b in m.bonuses)
            {
                stats.AddBonus(b.attributeName, new FinalBonus(b.value, b.multiplier));
            }
        }
    }

    public void AddModule(Module m)
    {
        modules.Add(m);
    }
    
}
