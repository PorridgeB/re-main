using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[ExecuteAlways]
public class Pie : Graphic, IBeginDragHandler, IDragHandler, IDropHandler
{
    public float Radius = 20;
    public float Thickness = 2;
    public int Rings = 3;
    public int Lines = 5;
    public int Segments = 16;
    public int UnlockedRings = 2;
    public Color LockedColor = Color.gray;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        for (int i = 0; i < Lines; i++)
        {
            var angle = Mathf.PI * (i / (float)Lines);

            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            var unlocked = direction * UnlockedRings * (Radius / Rings);
            var boundary = direction * Radius;

            GraphicShapes.AddLine(vh, color, -unlocked, unlocked, Thickness);
            GraphicShapes.AddLine(vh, LockedColor, -boundary, -unlocked, Thickness);
            GraphicShapes.AddLine(vh, LockedColor, unlocked, boundary, Thickness);
        }

        for (int i = 0; i < Rings; i++)
        {
            var ringColor = i < UnlockedRings ? color : LockedColor;

            GraphicShapes.AddCircle(vh, ringColor, Radius * ((i + 1) / (float)Rings), Thickness, Segments);
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

    public Vector2Int MousePosition()
    {
        var relMousePos = (Mouse.current.position.ReadValue() - new Vector2(transform.position.x, transform.position.y)) * 0.25f;
        return ToCoordinates(relMousePos);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        var softwareUpgrade = eventData.pointerDrag.GetComponent<Drag>().SoftwareUpgrade;

        SendMessageUpwards("OnSoftwareUpgradeDrop", new SoftwareUpgradeInstance { SoftwareUpgrade = softwareUpgrade, Position = MousePosition() });

        Debug.Log($"{softwareUpgrade.Name} at {MousePosition()}");
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SendMessageUpwards("OnSoftwareUpgradePieBeginDrag", eventData);
    }
}
