using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource : FloatReference
{
    public Attribute maxValue;
    public void ChangeValue(float value)
    {
        Variable.Value += value;
        Clamp();
    }

    public void Clamp()
    {
        if (Variable.Value > maxValue.Value())
        {
            Variable.Value = maxValue.Value();
        }
        else if (Variable.Value < 0)
        {
            Variable.Value = 0;
        }
    }

    public void SetValue(float value)
    {
        Variable.Value = value;
    }

    public void Reset()
    {
        Variable.Value = maxValue.Value();
    }
}
