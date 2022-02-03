using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GadgetRow : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI cost;

    [HideInInspector]
    public Gadget Gadget;

    private void Start()
    {
        name.text = Gadget.Name;
        cost.text = $"{Gadget.Cost} <sprite=1>";
    }

    public void Select()
    {
        SendMessageUpwards("SelectGadget", Gadget);
    }
}