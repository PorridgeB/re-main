using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GadgetInfo : MonoBehaviour
{
    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI cost;
    [SerializeField]
    private GameObject buy;
    [SerializeField]
    private GameObject equip;

    public void ShowGadget(Gadget gadget)
    {
        if (gadget == null)
        {
            name.text = "Name";
            description.text = "Description";
            cost.text = "100 <sprite=1>";

            return;
        }

        name.text = gadget.Name;
        description.text = gadget.Description;
        cost.text = $"{gadget.Cost} <sprite=1>";
    }

    public void Buy()
    {

    }

    public void Equip()
    {

    }
}
