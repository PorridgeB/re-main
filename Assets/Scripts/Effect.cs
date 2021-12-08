using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public float duration;
    public float chance;

    public virtual void Resolve(GameObject target) 
    {
    }
}
