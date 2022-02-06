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

    public static void AddArcWithOutline(VertexHelper vh, Color color, float outerRadius, float innerRadius, float minArcAngle, float maxArcAngle, int segments, Color outlineColor, float outlineThickness)
    {
        AddArc(vh, color, outerRadius, innerRadius, minArcAngle, maxArcAngle, segments);

        // Fill in the top and bottom outlines
        AddArc(vh, outlineColor, outerRadius, outerRadius - outlineThickness, minArcAngle, maxArcAngle, segments);
        AddArc(vh, outlineColor, innerRadius + outlineThickness, innerRadius, minArcAngle, maxArcAngle, segments);

        // Fill in the left and right outlines
        var rightSide = new Vector2(Mathf.Cos(minArcAngle * Mathf.Deg2Rad), Mathf.Sin(minArcAngle * Mathf.Deg2Rad));
        var leftSide = new Vector2(Mathf.Cos(maxArcAngle * Mathf.Deg2Rad), Mathf.Sin(maxArcAngle * Mathf.Deg2Rad));

        AddLine(vh, outlineColor, rightSide * innerRadius, rightSide * outerRadius, outlineThickness, 0);
        AddLine(vh, outlineColor, leftSide * innerRadius, leftSide * outerRadius, 0, outlineThickness);
    }

    public static void AddQuad(VertexHelper vh, Color color, Vector2 v1, Vector2 v2, Vector2 v3, Vector3 v4)
    {
        var vert = UIVertex.simpleVert;
        vert.color = color;

        var baseIndex = vh.currentVertCount;

        vert.position = v1;
        vh.AddVert(vert);

        vert.position = v2;
        vh.AddVert(vert);

        vert.position = v3;
        vh.AddVert(vert);

        vert.position = v4;
        vh.AddVert(vert);

        vh.AddTriangle(baseIndex, baseIndex + 3, baseIndex + 1);
        vh.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
    }

    public static void AddCircle(VertexHelper vh, Color color, float radius, float thickness, int segments)
    {
        AddArc(vh, color, radius + thickness * 0.5f, radius - thickness * 0.5f, 0, 360, segments);
    }

    public static void AddLine(VertexHelper vh, Color color, Vector2 from, Vector2 to, float thickness)
    {
        var perpendicular = Vector2.Perpendicular(to - from).normalized;

        var v1 = from + perpendicular * thickness * 0.5f;
        var v2 = from - perpendicular * thickness * 0.5f;
        var v3 = to + perpendicular * thickness * 0.5f;
        var v4 = to - perpendicular * thickness * 0.5f;

        AddQuad(vh, color, v1, v2, v3, v4);
    }

    public static void AddLine(VertexHelper vh, Color color, Vector2 from, Vector2 to, float leftThickness, float rightThickness)
    {
        var perpendicular = Vector2.Perpendicular(to - from).normalized;

        var v1 = from + perpendicular * leftThickness;
        var v2 = from - perpendicular * rightThickness;
        var v3 = to + perpendicular * leftThickness;
        var v4 = to - perpendicular * rightThickness;

        AddQuad(vh, color, v1, v2, v3, v4);
    }
}
