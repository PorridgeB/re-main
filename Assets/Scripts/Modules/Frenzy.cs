using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenzy : MonoBehaviour
{
    private float timer;
    [SerializeField]
    private float baseDuration;
    [SerializeField]
    private Attribute attackSpeed;
    [SerializeField]
    private float stackingDuration;
    [SerializeField]
    private Bonus bonusSpeed;
    [SerializeField]
    private Module module;
    private bool active;

    public void Effect()
    {
        if (!attackSpeed.HasTemporaryBonus(bonusSpeed))
        {
            attackSpeed.AddTemporaryBonus(bonusSpeed);
        }
        timer = (module.count > 0 ? baseDuration : 0) + stackingDuration * module.count-1;
        active = true;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && active)
        {
            active = false;
            attackSpeed.RemoveTemporaryBonus(bonusSpeed);
        } 
    }
}
