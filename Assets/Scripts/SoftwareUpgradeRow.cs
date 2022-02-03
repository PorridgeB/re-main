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
    private TextMeshProUGUI counter;

    public void OnBeginDrag(PointerEventData eventData)
    {
        SendMessageUpwards("OnSoftwareUpgradeRowBeginDrag", new BeginDragData { eventData = eventData, softwareUpgrade = SoftwareUpgrade });
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    private void Start()
    {
        name.text = SoftwareUpgrade.Name;
        counter.text = "0";
    }
}
