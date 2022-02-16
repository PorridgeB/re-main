using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Memory : MonoBehaviour
{
    [SerializeField]
    private List<Observation> observations = new List<Observation>();

    private void Update()
    {
        // Remove all expired and invalid observations
        observations.RemoveAll(x => x.Expired || x.Who == null);
    }

    private void OnDrawGizmosSelected()
    {
        var colors = new Dictionary<string, Color>()
        {
            { "Projectile", Color.blue },
            { "Player", Color.green },
            { "Enemy", Color.red },
            { "Cover", new Color(255, 128, 0) },
        };

        foreach (var observation in observations)
        {
            //Handles.color = colors.TryGetValue(observation.Who.tag, out Color color) ? color : Color.grey;

            var discRadius = observation.TimeLeftPercentage * 0.5f;

            if (Vector3.Distance(observation.Where, observation.Who.transform.position) > 2)
            {
                //Handles.DrawWireDisc(observation.Where, Vector3.up, discRadius);
                //Handles.DrawDottedLine(observation.Where, observation.Who.transform.position, 4);
            }

            //Handles.DrawWireDisc(observation.Who.transform.position, Vector3.up, discRadius);
        }
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
