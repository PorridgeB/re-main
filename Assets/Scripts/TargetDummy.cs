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

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("DamageSource"))
        {
            
            List<DamageInstance> sources = collision.gameObject.GetComponent<DamageSource>().Damages;
            foreach (DamageInstance d in sources)
            {
                Debug.Log("hit");
                damageTotal += d.value;
                GameObject g = Instantiate(damageToken, transform.position, new Quaternion());
                g.GetComponent<DamageToken>().SetValue(d);
            }
            
        }
    }
}
