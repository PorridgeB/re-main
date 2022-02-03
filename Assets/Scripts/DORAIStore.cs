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

    // If failed replacement, go to old position
    // Split drag prefab up into pieces visually

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
    }

    private void Place(SoftwareUpgradeInstance instance)
    {
        var wedge = Instantiate(wedgePrefab, pie.transform).GetComponent<Wedge>();

        instance.Object = wedge.gameObject;

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
        foreach (var line in new int[] { 0, -1, 1 })
        {
            foreach (var ring in new int[] { 0, -1, 1 })
            {
                var newPosition = new Vector2Int((instance.Position.x + line) % (pie.Lines * 2), Mathf.Max(0, instance.Position.y + ring));

                var newInstance = new SoftwareUpgradeInstance { Object = instance.Object, SoftwareUpgrade = instance.SoftwareUpgrade, Position = newPosition };

                if (CanPlace(newInstance))
                {
                    Place(newInstance);

                    instances.Add(instance);

                    return;
                }
            }
        }

        //if (!CanPlace(instance))
        //{
        //    return;
        //}

        //Place(instance);

        //instances.Add(instance);
    }

    public bool CanPlace(SoftwareUpgradeInstance instance)
    {
        if (instance.Position.y + instance.SoftwareUpgrade.Rings > pie.Rings)
        {
            return false;
        }

        return instances.TrueForAll(x => !Intersects(x, instance));
    }

    public bool Intersects(SoftwareUpgradeInstance a, SoftwareUpgradeInstance b)
    {
        return Occupied(a).Intersect(Occupied(b)).Any();
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

    public SoftwareUpgradeInstance PieceAt(Vector2Int position)
    {
        return instances.FirstOrDefault(x => Occupied(x).Contains(position));
    }

    public void OnSoftwareUpgradePieBeginDrag(PointerEventData eventData)
    {
        var piece = PieceAt(pie.MousePosition());
        if (piece == null)
        {
            return;
        }

        instances.Remove(piece);
        Destroy(piece.Object);

        OnSoftwareUpgradeRowBeginDrag(new SoftwareUpgradeRow.BeginDragData { eventData = eventData, softwareUpgrade = piece.SoftwareUpgrade });
    }

    public void Clear()
    {
        foreach (var piece in instances)
        {
            Destroy(piece.Object);
        }

        instances.Clear();
    }
}
