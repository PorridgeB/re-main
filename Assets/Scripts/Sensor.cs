using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [Tooltip("Rate of sensor update")]
    public float Rate = 0.5f;

    protected Memory memory;

    private void Awake()
    {
        memory = GetComponent<Memory>();
    }

    private void Start()
    {
        InvokeRepeating("Sense", 0, Rate);
    }

    public virtual void Sense()
    {
    }
}
