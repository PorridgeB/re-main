using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoftwareUpgradeInstance
{
    public SoftwareUpgrade SoftwareUpgrade;
    public Vector2Int Position; // X - Line, Y - Ring
    public GameObject Object;
}