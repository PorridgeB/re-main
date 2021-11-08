using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ModuleInventory : MonoBehaviour
{
    private List<List<float>> baseStats = new List<List<float>>();
    private List<List<float>> playerStats = new List<List<float>>();

    private PlayerStats stats;
    [SerializeField]
    private List<Module> modules;

    // Start is called before the first frame update
    void Start()
    {
        baseStats.Add(new List<float>() {16, 0, 0, -1});        //melee damage 
        baseStats.Add(new List<float>() { 0.5f, 0, 0, 3 });     //melee speed
        baseStats.Add(new List<float>() { 25, 0, 0, -1 });      //ranged damage
        baseStats.Add(new List<float>() { 1, 0, 0, 3 });        //ranged speed
        baseStats.Add(new List<float>() { 5, 0, 1, 20 });       //run speed
        baseStats.Add(new List<float>() { 2, 0, 0.5f, 20 });    //walk speed
        baseStats.Add(new List<float>() { 0.05f, 2, 0, -1 });   //crit chance
        baseStats.Add(new List<float>() { 1.5f, 2, 1, -1 });    //crit damage
        baseStats.Add(new List<float>() { 140, 1, 0, -1 });     //health
        baseStats.Add(new List<float>() { 2, 0, 0, -1 });       //health regen
        baseStats.Add(new List<float>() { 0.05f, 2, 0, 1 });    //dodge chance
        baseStats.Add(new List<float>() { 0.1f, 2, 0, 1 });     //resistance physical
        baseStats.Add(new List<float>() { 0.1f, 2, 0, 1 });     //resistance energy
        baseStats.Add(new List<float>() { 0.1f, 2, 0, 1 });     //resistance element
        playerStats.Add(new List<float>() { 100, 1, 0, -1 });   //power
        playerStats.Add(new List<float>() { 1.5f, 0, 0, -1 });  //power regen
        playerStats.Add(new List<float>() { 1, 1, 1, -1 });     //dash charges
        playerStats.Add(new List<float>() { 5, 0, 0, -1 });     //dash recharge rate
        stats = new PlayerStats(playerStats, baseStats);
    }


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
