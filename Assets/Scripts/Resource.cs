using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool depleted;

    [SerializeField]
    private string attributeName;

    private bool invunerable;
    [SerializeField]
    private float maxValue;
    private float currentValue;

    // Start is called before the first frame update
    void Start()
    {
        //this need to be updated by an event, not just once on start.
        maxValue = GetComponentInParent<PlayerStats>().ReadAttribute(attributeName);
        currentValue = maxValue;
    }

    public void ChangeValue(float amount)
    {
        if (invunerable) return;

        float previousValue = currentValue;
        currentValue = Mathf.Clamp(currentValue + amount, 0, maxValue);

        if (previousValue != 0 && currentValue <= 0)
        {
            depleted = true;
        }
    }

    public float GetPercentage()
    {
        return currentValue / maxValue;
    }

    public void Hide()
    {

    }

    public void Reveal()
    {

    }
}
