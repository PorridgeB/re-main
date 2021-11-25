using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private Timer cooldown;

    public bool Ready
    {
        get
        {
            return cooldown.Finished;
        }
    }

    public void Activate(float cooldownValue)
    {
        cooldown.Reset(cooldownValue);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
