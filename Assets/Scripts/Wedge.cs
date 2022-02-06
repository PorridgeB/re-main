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

        //var outlineColor = new Color(color.r * 0.5f, color.g * 0.5f, color.b * 0.5f);
        
        //var a1 = new Vector2(Mathf.Cos(MinArcAngle * Mathf.Deg2Rad), Mathf.Sin(MinArcAngle * Mathf.Deg2Rad)) * InnerRadius;
        //var a2 = new Vector2(Mathf.Cos(MinArcAngle * Mathf.Deg2Rad), Mathf.Sin(MinArcAngle * Mathf.Deg2Rad)) * OuterRadius;

        //var b1 = new Vector2(Mathf.Cos(MaxArcAngle * Mathf.Deg2Rad), Mathf.Sin(MaxArcAngle * Mathf.Deg2Rad)) * InnerRadius;
        //var b2 = new Vector2(Mathf.Cos(MaxArcAngle * Mathf.Deg2Rad), Mathf.Sin(MaxArcAngle * Mathf.Deg2Rad)) * OuterRadius;

        //GraphicShapes.AddLine(vh, outlineColor, a1, a2, 1);
        //GraphicShapes.AddLine(vh, outlineColor, b1, b2, 1);
    }
}
