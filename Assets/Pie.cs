using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Pie : Graphic
{
    public float Radius = 20;
    public float Thickness = 2;
    public int Layers = 3;
    public int Lines = 5;
    public int Segments = 16;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        for (int i = 0; i < Layers; i++)
        {
            GraphicShapes.AddCircle(vh, color, Radius * ((i + 1) / (float)Layers), Thickness, Segments);
        }

        for (int i = 0; i < Lines; i++)
        {
            var angle = Mathf.PI * (i / (float)Lines);

            var from = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;
            var to = -from;

            GraphicShapes.AddLine(vh, color, from, to, Thickness);
        }
    }
}
