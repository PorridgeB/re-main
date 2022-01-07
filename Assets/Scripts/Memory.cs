using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField]
    private float expiryTime = 1.5f;
    [SerializeField]
    private List<Observation> observations = new List<Observation>();

    private void Update()
    {
        observations.RemoveAll(x => Time.time - x.Time > expiryTime);
    }

    public Observation WithTag(string tag)
    {
        return observations.Find(x => x.GameObject?.tag == tag);
    }

    public void Record(Observation observation)
    {
        observations.Add(observation);
    }
}
