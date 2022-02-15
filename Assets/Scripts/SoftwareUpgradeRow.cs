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

    private Button softwareButton;
    private EventTrigger eventTrigger;

    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI cost;
    [SerializeField]
    private GameObject buyOverlay;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Unlocked) SendMessageUpwards("OnSoftwareUpgradeRowBeginDrag", new BeginDragData { eventData = eventData, softwareUpgrade = SoftwareUpgrade });
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void Buy()
    {
        SendMessageUpwards("OnSoftwareUpgradeBuy", SoftwareUpgrade);
        if (!Unlocked)
        {
            softwareButton.enabled = true;
            eventTrigger.enabled = true;
        }
    }

    private void Start()
    {
        name.text = SoftwareUpgrade.Name;
        cost.text = $"{SoftwareUpgrade.Cost} <sprite=0 tint>";

        softwareButton = GetComponent<Button>();
        softwareButton.interactable = Unlocked;
        softwareButton.enabled = Unlocked;

        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.enabled = Unlocked;

        buyOverlay.SetActive(!Unlocked);
        
    }
}
