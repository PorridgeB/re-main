using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReserves : MonoBehaviour
{
    [SerializeField]
    private float timer;
    [SerializeField]
    private float idleDuration;
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private float stackingDamage;
    [SerializeField]
    private Module module;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (module.count > 0)
        {
            timer += Time.deltaTime;
        }
        
    }

    public void Effect(DamageSource source)
    {
        
        if (source.source == PlayerController.instance.gameObject)
        {
            if (timer > idleDuration)
            {
                Debug.Log("triggered power reserves");
                DamageInstance d = new DamageInstance();
                d.source = gameObject;
                d.value = baseDamage + (stackingDamage * (module.count - 1));
                source.AddInstance(d);
                
            }
            timer = 0;
        }
    }
}
