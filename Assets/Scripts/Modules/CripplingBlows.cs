using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripplingBlows : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float baseChance;
    [SerializeField]
    private float stackingChance;
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

    public void Effect(GameObject gameObject)
    {
        if (module.count > 0)
        {
            Debug.Log(baseChance + stackingChance * (module.count - 1) + " : " + Random.value);
            if (Random.value < baseChance + (stackingChance * (module.count - 1)))
            {
                gameObject.GetComponent<Drone>().Slow();
            }
            
        }
    }
}
