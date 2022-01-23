using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GadgetRow : MonoBehaviour
{
    public Gadget Gadget;

    private void Start()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();

        text.text = Gadget.Name;
    }

    public void Select()
    {
        SendMessageUpwards("SelectGadget", Gadget);
    }
}
