using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Health
{
    public GameEvent HealthDepleted;

    private bool invunerable;
    [SerializeField]
    private Attribute max;
    [SerializeField]
    private FloatVariable current;

    // Start is called before the first frame update
    void Start()
    {
        current.Value = max.Value();
    }

    public void ChangeValue(float amount)
    {
        if (invunerable) return;

        float previousValue = current.Value;
        current.Value = Mathf.Clamp(current.Value + amount, 0, max.Value());

        if (previousValue != 0 && current.Value <= 0)
        {
            HealthDepleted.Raise();
        }
    }

    public float GetPercentage()
    {
        return current.Value / max.Value();
    }
}
