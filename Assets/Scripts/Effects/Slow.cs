using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Effect
{
    public Slow(float slowChance, float slowDuration)
    {
        chance = slowChance;
        duration = slowDuration;
    }
    public override void Resolve(GameObject target)
    {
        if (Random.value < chance) target.GetComponent<Drone>().Slow(duration);
    }
}
