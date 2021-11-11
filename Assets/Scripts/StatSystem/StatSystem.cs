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
		attributes = new Dictionary<string, Attribute>();
		for (int i = 0; i < attributeNames.Count; i++)
		{
			AddAttribute(attributeNames[i], stat_block[i]);
		}
	}

	public void AddAttribute(string name, List<float> statValues)
	{
		attributes[name] = new Attribute(statValues[0], (DisplayType)statValues[1], statValues[0], statValues[0]);
		attributes[name].name = name;
	}

	public void AddFinalBonus(string name, FinalBonus bonus)
	{
		attributes[name].AddFinalBonus(bonus);
	}

	public int Count()
	{
		return attributes.Count;
	}

	public void ClearFinalBonuses()
	{
		foreach (KeyValuePair<string, Attribute> a in attributes)
		{
			a.Value.ClearFinalBonuses();
		}
	}

	public Attribute[] GetAttributeAsArray()
	{
		Attribute[] a = new Attribute[attributes.Count];
		attributes.Values.CopyTo(a, 0);
		return a;
	}
}
