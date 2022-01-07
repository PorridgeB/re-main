using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Observation
{
    // Time when the observation was made
    public float When;
    public Vector3 Where;
    public GameObject Who;

    public Observation(Vector3 where, GameObject who)
    {
        When = Time.time;
        Where = where;
        Who = who;
    }
}
