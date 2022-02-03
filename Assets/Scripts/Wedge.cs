using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
public class Wedge : Graphic
{
    public float OuterRadius = 10;
    public float InnerRadius = 5;
    public float MinArcAngle = 0;
    public float MaxArcAngle = 180;
    [Range(1, 64)]
    public int Segments = 16;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        GraphicShapes.AddArc(vh, color, OuterRadius, InnerRadius, MinArcAngle, MaxArcAngle, Segments);
    }
}
