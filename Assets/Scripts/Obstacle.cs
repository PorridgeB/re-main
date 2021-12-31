using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject attackField;
    [SerializeField]
    private DamageInstance damage;
    [SerializeField]
    private float tickRate;
    private float timer;
    private GameObject instance;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tickRate)
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            timer = 0;
            instance = Instantiate(attackField, transform.position, new Quaternion(), null);
            instance.GetComponent<DamageSource>().AddInstance(damage);
        }
    }
}
