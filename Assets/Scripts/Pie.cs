using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[ExecuteAlways]
public class Pie : Graphic, IDropHandler
{
    public float Radius = 20;
    public float Thickness = 2;
    public int Rings = 3;
    public int Lines = 5;
    public int Segments = 16;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        for (int i = 0; i < Rings; i++)
        {
            GraphicShapes.AddCircle(vh, color, Radius * ((i + 1) / (float)Rings), Thickness, Segments);
        }

        for (int i = 0; i < Lines; i++)
        {
            var angle = Mathf.PI * (i / (float)Lines);

            var from = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;
            var to = -from;

            GraphicShapes.AddLine(vh, color, from, to, Thickness);
        }
    }

    public Vector2Int ToCoordinates(Vector2 position)
    {
        var distance = position.magnitude;
        var angle = Vector2.SignedAngle(Vector2.right, position);

        // Avoids negative coordinates
        if (angle < 0)
        {
            angle += 360;
        }

        var ring = Mathf.FloorToInt(distance / (Radius / Rings));
        var line = Mathf.FloorToInt(angle / (180f / Lines));

        return new Vector2Int(line, ring);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var softwareUpgrade = eventData.pointerDrag.GetComponent<Drag>().SoftwareUpgrade;

        var relMousePos = (Mouse.current.position.ReadValue() - new Vector2(transform.position.x, transform.position.y)) * 0.25f;



        Debug.Log($"{softwareUpgrade.Name} at {ToCoordinates(relMousePos)}");
    }
}
