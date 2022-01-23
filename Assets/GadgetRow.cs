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

    public Gadget Gadget;

    private void Start()
    {
        name.text = Gadget.Name;
        cost.text = Gadget.Cost.ToString();
    }

    public void Select()
    {
        SendMessageUpwards("SelectGadget", Gadget);
    }
}
