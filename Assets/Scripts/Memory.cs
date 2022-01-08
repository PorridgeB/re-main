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
        // Remove all expired observations
        observations.RemoveAll(x => Time.time - x.When > expiryTime);
    }

    public Observation FirstWithTag(string tag)
    {
        return observations.Find(x => x.Who != null && x.Who.CompareTag(tag));
    }

    public List<Observation> WithTag(string tag)
    {
        return observations.FindAll(x => x.Who != null && x.Who.CompareTag(tag));
    }

    // Adds a new observation.
    // Any observation with the same `Who` will be replaced
    public void Record(Observation observation)
    {
        observations.RemoveAll(x => x.Who == observation.Who);
        observations.Add(observation);
    }
}
