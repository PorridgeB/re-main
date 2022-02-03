using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DORAIStore : MonoBehaviour
{
    [SerializeField]
    private GameObject softwareUpgradeRowPrefab;
    [SerializeField]
    private GameObject softwareUpgradeList;
    [SerializeField]
    private TextMeshProUGUI dataFragments;
    [SerializeField]
    private Pie pie;
    [SerializeField]
    private GameObject wedgePrefab;
    [SerializeField]
    private GameObject dragPrefab;

    // Buy upgrades
    // Drag out upgrade from list

    private void Start()
    {
        var softwareUpgrades = Resources.LoadAll<SoftwareUpgrade>("SoftwareUpgrades");

        foreach (var softwareUpgrade in softwareUpgrades)
        {
            var softwareUpgradeRow = Instantiate(softwareUpgradeRowPrefab, softwareUpgradeList.transform).GetComponent<SoftwareUpgradeRow>();
            softwareUpgradeRow.SoftwareUpgrade = softwareUpgrade;
        }

        var softwareUpgradeInst1 = new SoftwareUpgradeInstance { SoftwareUpgrade = softwareUpgrades[0], Position = new Vector2Int(0, 0) };
        var softwareUpgradeInst2 = new SoftwareUpgradeInstance { SoftwareUpgrade = softwareUpgrades[1], Position = new Vector2Int(2, 1) };

        var softwareUpgradeInsts = new List<SoftwareUpgradeInstance>() { softwareUpgradeInst1, softwareUpgradeInst2 };

        foreach (var softwareUpgradeInst in softwareUpgradeInsts)
        {
            var wedge = Instantiate(wedgePrefab, pie.transform).GetComponent<Wedge>();

            var softwareUpgrade = softwareUpgradeInst.SoftwareUpgrade;
            var line = softwareUpgradeInst.Position.x;
            var ring = softwareUpgradeInst.Position.y;

            wedge.InnerRadius = ring * (pie.Radius / pie.Rings);
            wedge.OuterRadius = (ring + softwareUpgrade.Rings) * (pie.Radius / pie.Rings);
            wedge.MinArcAngle = line * (180f / pie.Lines);
            wedge.MaxArcAngle = (line + softwareUpgrade.Lines) * (180f / pie.Lines);
            wedge.color = softwareUpgrade.Color;

            wedge.SetAllDirty();
        }
    }

    private void Update()
    {
        // canvas.scaleFactor

        var relMousePos = (Mouse.current.position.ReadValue() - new Vector2(pie.transform.position.x, pie.transform.position.y)) * 0.25f;

        //Debug.Log(pie.ToCoordinates(relMousePos));
    }

    public void OnSoftwareUpgradeRowBeginDrag(SoftwareUpgradeRow.BeginDragData data)
    {
        var eventData = data.eventData;

        var go = Instantiate(dragPrefab, eventData.position, Quaternion.identity);
        go.transform.SetParent(transform, false);
        eventData.pointerDrag = go;

        go.GetComponent<Drag>().SoftwareUpgrade = data.softwareUpgrade;
    }
}
