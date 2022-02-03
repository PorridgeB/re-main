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

    private List<SoftwareUpgradeInstance> instances;

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
        var softwareUpgradeInst2 = new SoftwareUpgradeInstance { SoftwareUpgrade = softwareUpgrades[1], Position = new Vector2Int(9, 1) };
        var softwareUpgradeInst3 = new SoftwareUpgradeInstance { SoftwareUpgrade = softwareUpgrades[2], Position = new Vector2Int(2, 1) };

        Debug.Log(string.Join(", ", Occupied(softwareUpgradeInst3).Select(x => x.ToString())));

        instances = new List<SoftwareUpgradeInstance>() { softwareUpgradeInst1, softwareUpgradeInst2, softwareUpgradeInst3 };

        foreach (var instance in instances)
        {
            Place(instance);
        }
    }

    private void Update()
    {
        // canvas.scaleFactor

        var relMousePos = (Mouse.current.position.ReadValue() - new Vector2(pie.transform.position.x, pie.transform.position.y)) * 0.25f;

        //Debug.Log(pie.ToCoordinates(relMousePos));
    }

    private void Place(SoftwareUpgradeInstance instance)
    {
        var wedge = Instantiate(wedgePrefab, pie.transform).GetComponent<Wedge>();

        var softwareUpgrade = instance.SoftwareUpgrade;
        var line = instance.Position.x;
        var ring = instance.Position.y;

        wedge.InnerRadius = ring * (pie.Radius / pie.Rings);
        wedge.OuterRadius = (ring + softwareUpgrade.Rings) * (pie.Radius / pie.Rings);
        wedge.MinArcAngle = line * (180f / pie.Lines);
        wedge.MaxArcAngle = (line + softwareUpgrade.Lines) * (180f / pie.Lines);
        wedge.color = softwareUpgrade.Color;

        wedge.SetAllDirty();
    }

    public void OnSoftwareUpgradeRowBeginDrag(SoftwareUpgradeRow.BeginDragData data)
    {
        var eventData = data.eventData;

        var go = Instantiate(dragPrefab, eventData.position, Quaternion.identity);
        go.transform.SetParent(transform, false);
        eventData.pointerDrag = go;

        go.GetComponent<Drag>().SoftwareUpgrade = data.softwareUpgrade;
    }

    public void OnSoftwareUpgradeDrop(SoftwareUpgradeInstance instance)
    {
        if (!CanPlace(instance))
        {
            Debug.Log("Occupied");
            return;
        }

        Place(instance);

        instances.Add(instance);
    }

    public bool CanPlace(SoftwareUpgradeInstance instance)
    {
        if (instance.Position.y + instance.SoftwareUpgrade.Rings > pie.Rings)
        {
            return false;
        }

        foreach (var x in instances)
        {
            if (Intersects(instance, x))
            {
                return false;
            }
        }

        return true;

        //return instances.TrueForAll(x => !Intersects(x, instance));
    }

    public bool Intersects(SoftwareUpgradeInstance a, SoftwareUpgradeInstance b)
    {
        foreach (var aCell in Occupied(a))
        {
            foreach (var bCell in Occupied(b))
            {
                if (aCell == bCell)
                {
                    return true;
                }
            }
        }

        return false;

        //return Occupied(a).Intersect(Occupied(b)).Any();
    }

    // Enumerates over all the positions that the software upgrade piece occupies
    public IEnumerable<Vector2Int> Occupied(SoftwareUpgradeInstance instance)
    {
        for (int line = 0; line < instance.SoftwareUpgrade.Lines; line++)
        {
            for (int ring = 0; ring < instance.SoftwareUpgrade.Rings; ring++)
            {
                yield return new Vector2Int((instance.Position.x + line) % (pie.Lines * 2), instance.Position.y + ring);
            }
        }
    }
}
