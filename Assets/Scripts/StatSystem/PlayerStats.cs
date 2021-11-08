using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : StatSystem
{
	private static List<string> attributeNames = new List<string>()
	{
		"Power",
		"Power Regen",
		"Dash Charges",
		"Dash Recharge Rate"
	};

	public PlayerStats(List<List<float>> playerStats, List<List<float>> baseStats) : base (baseStats)
    {
		for (int i = 0; i < attributeNames.Count; i++)
		{
			attributes.Add(attributeNames[i], new Attribute(playerStats[0][0], (DisplayType)playerStats[0][1], playerStats[0][2], playerStats[0][3]));
		}
    }

	public void AddBonus(string name, FinalBonus bonus)
	{
		attributes[name].AddFinalBonus(bonus);
	}
}
