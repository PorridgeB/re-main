using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoftwareUpgrade : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;
    public Color Color = Color.white;
    [Min(1)]
    public int Points = 1;
}
