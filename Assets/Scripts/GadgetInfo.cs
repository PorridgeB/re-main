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

    public void ShowGadget(Gadget gadget)
    {
        currentGadget = gadget;

        if (gadget == null)
        {
            name.text = "Name";
            description.text = "Description";
            cost.text = "100 <sprite=1 tint>";

            return;
        }

        var save = SaveManager.Instance.Save;

        var canBuy = save.Scrap >= gadget.Cost;
        buyButton.interactable = canBuy;

        name.text = gadget.Name;
        description.text = gadget.Description;
        cost.text = $"{gadget.Cost} <sprite=1 tint>";
        cost.color = canBuy ? sufficientColor : insufficientColor;

        var hasGadget = save.UnlockedGadgets.Contains(gadget.name);
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
