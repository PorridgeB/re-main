using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chart : MonoBehaviour
{
    public Pie Pie;
    public Wedge Wedge;
    public float Speed = 20;

    private void Update()
    {
        var mousePos = Mouse.current.position.ReadValue() - new Vector2(Screen.width, Screen.height) * 0.5f;

        var distance = mousePos.magnitude * 0.25f;
        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        var ring = Mathf.Floor(distance / (Pie.Radius / Pie.Rings));
        var line = Mathf.Floor(angle / (180 / Pie.Lines));

        Wedge.enabled = ring < Pie.Rings;

        //Wedge.OuterRadius = (ring + 1) * (Pie.Radius / Pie.Rings);
        //Wedge.InnerRadius = ring * (Pie.Radius / Pie.Rings);
        //Wedge.MinArcAngle = line * (180 / Pie.Lines);
        //Wedge.MaxArcAngle = (line + 1) * (180 / Pie.Lines);

        var outerRadius = (ring + 1) * (Pie.Radius / Pie.Rings);
        var innerRadius = ring * (Pie.Radius / Pie.Rings);
        
        var minArcAngle = line * (180 / Pie.Lines);
        var maxArcAngle = (line + 1) * (180 / Pie.Lines);

        Wedge.OuterRadius = Mathf.Lerp(Wedge.OuterRadius, outerRadius, Speed * Time.deltaTime);
        Wedge.InnerRadius = Mathf.Lerp(Wedge.InnerRadius, innerRadius, Speed * Time.deltaTime);
        Wedge.MinArcAngle = Mathf.Lerp(Wedge.MinArcAngle, minArcAngle, Speed * Time.deltaTime);
        Wedge.MaxArcAngle = Mathf.Lerp(Wedge.MaxArcAngle, maxArcAngle, Speed * Time.deltaTime);

        Wedge.SetAllDirty();
    }
}
