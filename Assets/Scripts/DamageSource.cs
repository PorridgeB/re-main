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

    private void Awake()
    {
        source = transform.parent.gameObject;
        damageSourceCreated.Raise(this);
    }

    public List<DamageInstance> Damages
    {
        get
        {
            return damageInstances;
        }
    }

    public List<Effect> Effects
    {
        get
        {
            return effects;
        }
    }

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
