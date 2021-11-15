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

    public void Reset()
    {
        current = max;
    }

    public Timer(float _max)
    {
        max = _max;
    }

    // Update is called once per frame
    void Update()
    {
        current -= Time.deltaTime;
    }
}
