using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RunInfo
{
    public bool ended;
    public int difficulty;

    public int quadrant;
    public int sector;

    public int scrap;
    public int dataFragments;

    public int kills;
    public int damage;
    public int largestHit;

    public int rebootCount;

    public string killedBy;

    public float time;
}
