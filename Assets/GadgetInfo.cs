using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GadgetInfo : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;

    public void SetGadget(Gadget gadget)
    {
        Name.text = gadget.Name;
        Description.text = gadget.Description;
        Cost.text = gadget.Cost.ToString();
    }
}
