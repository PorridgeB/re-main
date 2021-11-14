using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[SerializeField]
	private int statCount;

	private static List<string> attributeNames = new List<string>()
	{
		"Power",
		"Power Regen",
		"Dash Charges",
		"Dash Recharge Rate"
	};

	private StatSystem stats;

    private List<List<float>> baseStats = new List<List<float>>();
    private List<List<float>> playerStats = new List<List<float>>();

    // Start is called before the first frame update
    void Awake()
    {
        baseStats.Add(new List<float>() { 16, 0, 0, -1 });        //melee damage 
        baseStats.Add(new List<float>() { 0.5f, 0, 0, 3 });     //melee speed
        baseStats.Add(new List<float>() { 25, 0, 0, -1 });      //ranged damage
        baseStats.Add(new List<float>() { 1, 0, 0, 3 });        //ranged speed
        baseStats.Add(new List<float>() { 2, 0, 1, 20 });       //run speed
        baseStats.Add(new List<float>() { 1, 0, 0.5f, 20 });    //walk speed
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
        Initialize(playerStats, baseStats);
    }

    public void Initialize(List<List<float>> playerStats, List<List<float>> baseStats)
	{
		stats = new StatSystem(baseStats);
		for (int i = 0; i < attributeNames.Count; i++)
		{
			stats.AddAttribute(attributeNames[i], playerStats[i]);
		}
		statCount = stats.Count();
	}

    public void ClearFinalBonuses()
    {
        stats.ClearFinalBonuses();
    }
    public int Count()
    {
        return stats.Count();
    }

	public void AddBonus(string name, FinalBonus bonus)
	{
		stats.AddFinalBonus(name, bonus);
	}

    public float ReadAttribute(string name)
    {
        return stats.ReadAttribute(name);
    }

    public Attribute[] GetAttributesAsArray()
    {
        return stats.GetAttributeAsArray();
    }
}
