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
    public float OutlineThickness = 1;
    [Range(1, 64)]
    public int Segments = 16;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        var outlineColor = new Color(color.r * 0.5f, color.g * 0.5f, color.b * 0.5f);
        
        GraphicShapes.AddArcWithOutline(vh, color, OuterRadius, InnerRadius, MinArcAngle, MaxArcAngle, Segments, outlineColor, OutlineThickness);
    }
}
