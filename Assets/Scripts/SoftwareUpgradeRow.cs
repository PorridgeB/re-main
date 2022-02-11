using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoftwareUpgradeRow : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public SoftwareUpgrade SoftwareUpgrade;
    public bool Unlocked = false;

    public class BeginDragData
    {
        public PointerEventData eventData;
        public SoftwareUpgrade softwareUpgrade;
    }

    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI cost;
    [SerializeField]
    private GameObject costObject;

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
        cost.text = $"{SoftwareUpgrade.Cost} <sprite=0 tint>";

        var button = GetComponent<Button>();
        button.interactable = !Unlocked;

        costObject.SetActive(!Unlocked);
    }
}
