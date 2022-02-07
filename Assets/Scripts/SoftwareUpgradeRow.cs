using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoftwareUpgradeRow : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public SoftwareUpgrade SoftwareUpgrade;

    public class BeginDragData
    {
        public PointerEventData eventData;
        public SoftwareUpgrade softwareUpgrade;
    }

    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI cost;

    public void OnBeginDrag(PointerEventData eventData)
    {
        SendMessageUpwards("OnSoftwareUpgradeRowBeginDrag", new BeginDragData { eventData = eventData, softwareUpgrade = SoftwareUpgrade });
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void Buy()
    {
        SendMessageUpwards("OnSoftwareUpgradeBuy", SoftwareUpgrade);
    }

    private void Start()
    {
        name.text = SoftwareUpgrade.Name;
        cost.text = $"{SoftwareUpgrade.Cost} <sprite=0>";
    }
}
