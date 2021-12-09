using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private float stackingDamage;
    [SerializeField]
    private Module module;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect()
    {
        if (module.count > 0)
        {
            DamageInstance d = new DamageInstance();
            d.source = PlayerController.instance.gameObject;
            d.value = baseDamage + (stackingDamage * (module.count - 1));
            PlayerController.instance.damageTaken[0].source.GetComponent<Enemy>().Hurt(d);
        }
        
    }
}
