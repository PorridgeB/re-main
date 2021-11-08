using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem
{
	private static List<string> attributeNames = new List<string>()
	{
		"Melee Attack Damage",
		"Melee Attack Speed",
		"Ranged Attack Damage",
		"Ranged Attack Speed",
		"Run Speed",
		"Walk Speed",
		"Crit Chance",
		"Crit Damage",
		"Health",
		"Health Regen",
		"Dodge Chance",
		"Resistance Physical",
		"Resistance Energy",
		"Resistance Elemental"
	};

	protected Dictionary<string, Attribute> attributes;

    public StatSystem(List<List<float>> stat_block)
    {
		for (int i = 0; i < attributeNames.Count; i++)
		{
			Debug.Log(i);
			attributes.Add(attributeNames[i], new Attribute(stat_block[0][0], (DisplayType)stat_block[0][1], stat_block[0][2], stat_block[0][3]));
		}
	}
}
