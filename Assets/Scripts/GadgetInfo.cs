using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GadgetInfo : MonoBehaviour
{
    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI cost;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private GameObject buy;
    [SerializeField]
    private GameObject equip;
    [SerializeField]
    private Color sufficientColor;
    [SerializeField]
    private Color insufficientColor;
    private Gadget currentGadget;

    public void ShowGadget(Gadget gadget, SaveSO save)
    {
        currentGadget = gadget;

        if (gadget == null)
        {
            name.text = "Name";
            description.text = "Description";
            cost.text = "100 <sprite=1 tint>";

            return;
        }

        buyButton.interactable = save.CanBuyWithScrap(gadget.Cost);

        name.text = gadget.Name;
        description.text = gadget.Description;
        cost.text = $"{gadget.Cost} <sprite=1 tint>";
        cost.color = save.CanBuyWithScrap(gadget.Cost) ? sufficientColor : insufficientColor;

        var hasGadget = save.GadgetIsUnlocked(gadget.name);
        buy.SetActive(!hasGadget);
        equip.SetActive(hasGadget);
    }

    public void Buy()
    {
        SendMessageUpwards("OnGadgetBuy", currentGadget);
    }

    public void Equip()
    {
        SendMessageUpwards("OnGadgetEquip", currentGadget);
    }
}
