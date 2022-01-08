using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [Tooltip("Rate of sensor update")]
    public float Rate = 0.25f;

    protected Memory memory;

    private void Awake()
    {
        memory = GetComponent<Memory>();
    }

    private void Start()
    {
        InvokeRepeating("Sense", Random.Range(0, Rate), Rate);
    }

    public virtual void Sense()
    {
    }
}
