using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
[ExecuteAlways]
public class Wedge : Graphic
{
    public float OuterRadius = 20;
    public float InnerRadius = 10;
    public float MinArcAngle = 0;
    public float MaxArcAngle = 45;
    public int Segments = 32;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Vector2 corner1 = Vector2.zero;
        Vector2 corner2 = Vector2.zero;

        corner1.x = 0f;
        corner1.y = 0f;
        corner2.x = 1f;
        corner2.y = 1f;

        corner1.x -= rectTransform.pivot.x;
        corner1.y -= rectTransform.pivot.y;
        corner2.x -= rectTransform.pivot.x;
        corner2.y -= rectTransform.pivot.y;

        corner1.x *= rectTransform.rect.width;
        corner1.y *= rectTransform.rect.height;
        corner2.x *= rectTransform.rect.width;
        corner2.y *= rectTransform.rect.height;

        vh.Clear();

        var vert = UIVertex.simpleVert;

        vert.position = new Vector2(0, 0);
        vert.color = color;
        vh.AddVert(vert);

        for (int i = 0; i < Segments + 1; i++)
        {
            var angle = Mathf.LerpAngle(MinArcAngle, MaxArcAngle, i / (float)Segments);

            vert.position = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * OuterRadius;
            vert.color = color;
            vh.AddVert(vert);

            if (i < Segments)
            {
                vh.AddTriangle(0, i + 2, i + 1);
            }
        }

        //    UIVertex vert = UIVertex.simpleVert;

        //    vert.position = new Vector2(corner1.x, corner1.y);
        //    vert.color = color;
        //    vh.AddVert(vert);

        //    vert.position = new Vector2(corner1.x, corner2.y);
        //    vert.color = color;
        //    vh.AddVert(vert);

        //    vert.position = new Vector2(corner2.x, corner2.y);
        //    vert.color = color;
        //    vh.AddVert(vert);

        //    vert.position = new Vector2(corner2.x, corner1.y);
        //    vert.color = color;
        //    vh.AddVert(vert);

        //    vh.AddTriangle(0, 1, 2);
        //    vh.AddTriangle(2, 3, 0);
        //}
    }
}
