using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Pie : Graphic
{
    public int Layers = 3;
    public int Lines = 5;
    public float Radius = 20;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
    }
}
