using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class DamageInstance : MonoBehaviour
{
    public GameObject source;
    public float value;
    public bool crit;
    public DamageType type;
}
