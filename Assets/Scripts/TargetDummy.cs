using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    [SerializeField]
    private float damageTotal;
    [SerializeField]
    private GameObject damageToken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageSource"))
        {
            int damage = collision.gameObject.GetComponent<DamageSource>().Value;
            damageTotal += damage;
            GameObject g = Instantiate(damageToken, transform.position, new Quaternion());
            g.GetComponent<DamageToken>().SetValue(damage);
            Destroy(collision.gameObject.GetComponent<Projectile>()?.gameObject);
        }
    }
}
