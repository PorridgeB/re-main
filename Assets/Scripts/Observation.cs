using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Observation
{
    // The GameObject that was observed
    public GameObject Who;
    // Time when the observation was made
    public float When;
    // Position of the GameObject when the observation was made
    public Vector3 Where;
    // How long the observation can last in memory before being forgotten
    public float ExpiryTime;

    // If the observation is no longer relevant and should be destroyed
    public bool Expired => Time.time - When > ExpiryTime;

    // The percentage of time left until the observation has expired
    public float TimeLeftPercentage => Mathf.Clamp01(1 - ((Time.time - When) / ExpiryTime));

    public Observation(GameObject who, float expiryTime)
    {
        Who = who;
        When = Time.time;
        Where = who.transform.position;
        ExpiryTime = expiryTime;
    }
}
