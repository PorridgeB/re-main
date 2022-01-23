using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        var vert = UIVertex.simpleVert;

        //vert.position = new Vector2();
        //vert.color = color;
        //vh.AddVert(vert);

        for (int i = 0; i < Segments + 1; i++)
        {
            var angle = (MinArcAngle + (MaxArcAngle - MinArcAngle) * (i / (float)Segments)) * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            vert.position = direction * InnerRadius;
            vert.color = color;
            vh.AddVert(vert);

            vert.position = direction * OuterRadius;
            vert.color = color;
            vh.AddVert(vert);

            if (i < Segments)
            {
                vh.AddTriangle(i * 2, i * 2 + 2, i * 2 + 1);
                vh.AddTriangle(i * 2 + 1, i * 2 + 2, i * 2 + 3);
            }
        }
    }
}
