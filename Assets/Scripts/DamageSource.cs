using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Energy,
    Elemental
}

public class DamageSource : MonoBehaviour
{
    [SerializeField]
    public GameObject source;
    private List<DamageInstance> damageInstances = new List<DamageInstance>();
    private List<Effect> effects = new List<Effect>();
    [SerializeField]
    private GameEvent damageSourceCreated;

    private void Start()
    {
        damageSourceCreated.Raise(this);
    }

    // Knockback force of the damage
    // TODO: Add multiplier and/or override for the knockback force
    public float Force => 1;

    public int Count => damageInstances.Count;
    public List<DamageInstance> Damages => damageInstances;
    public List<Effect> Effects => effects;

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }

    public void AddInstance(DamageInstance instance)
    {
        // Ignore damage instances from a different source
        if (instance.source != source)
        {
            return;
        }

        damageInstances.Add(instance);
    }
}
