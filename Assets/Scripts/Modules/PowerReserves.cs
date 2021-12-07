using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReserves : MonoBehaviour
{
    private float timer;
    [SerializeField]
    private AttackEvent nextAttack;
    [SerializeField]
    private float idleDuration;
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private float stackingDamage;
    [SerializeField]
    private Module module;
    private bool damageInstanceAdded;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > idleDuration && !damageInstanceAdded)
        {
            damageInstanceAdded = true;
            DamageInstance d = new DamageInstance();
            d.value = baseDamage+stackingDamage*module.count;
            d.source = PlayerController.instance.gameObject;
            nextAttack.damageInstances.Add(d);
        }
    }

    public void Effect()
    {
        timer = 0;
        damageInstanceAdded = false;
    }
}
