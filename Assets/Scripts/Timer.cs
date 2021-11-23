using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer : MonoBehaviour
{
    [SerializeField]
    private float max;
    [SerializeField]
    private float current;
    public bool Finished
    {
        get
        {
            return current < 0;
        }
        
    }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        current = max;
    }

    public float GetTime()
    {
        return current;
    }

    // Update is called once per frame
    void Update()
    {
        current -= Time.deltaTime;
    }
}
