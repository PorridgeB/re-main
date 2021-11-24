using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int value;

    public int Value
    {
        get
        {
            return value;
        }
    }

    public void SetValue(float damage) 
    {
        value = Mathf.RoundToInt(damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
