using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GraphicShapes
{
    public static void AddArc(VertexHelper vh, Color color, float outerRadius, float innerRadius, float minArcAngle, float maxArcAngle, int segments)
    {
        var vert = UIVertex.simpleVert;

        var baseIndex = vh.currentVertCount;

        for (int i = 0; i < segments + 1; i++)
        {
            var angle = (minArcAngle + (maxArcAngle - minArcAngle) * (i / (float)segments)) * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            vert.position = direction * innerRadius;
            vert.color = color;
            vh.AddVert(vert);

            vert.position = direction * outerRadius;
            vert.color = color;
            vh.AddVert(vert);

            if (i < segments)
            {
                vh.AddTriangle(baseIndex + i * 2, baseIndex + i * 2 + 2, baseIndex + i * 2 + 1);
                vh.AddTriangle(baseIndex + i * 2 + 1, baseIndex + i * 2 + 2, baseIndex + i * 2 + 3);
            }
        }
    }

    public static void AddCircle(VertexHelper vh, Color color, float radius, float thickness, int segments)
    {
        AddArc(vh, color, radius + thickness * 0.5f, radius - thickness * 0.5f, 0, 360, segments);
    }

    public static void AddLine(VertexHelper vh, Color color, Vector2 from, Vector2 to, float thickness)
    {
        var vert = UIVertex.simpleVert;

        var baseIndex = vh.currentVertCount;

        var perpDirection = Vector2.Perpendicular(to - from).normalized;

        vert.position = from + perpDirection * thickness * 0.5f;
        vert.color = color;
        vh.AddVert(vert);

        vert.position = from - perpDirection * thickness * 0.5f;
        vert.color = color;
        vh.AddVert(vert);

        vert.position = to + perpDirection * thickness * 0.5f;
        vert.color = color;
        vh.AddVert(vert);

        vert.position = to - perpDirection * thickness * 0.5f;
        vert.color = color;
        vh.AddVert(vert);

        vh.AddTriangle(baseIndex, baseIndex + 3, baseIndex + 1);
        vh.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
    }
}
