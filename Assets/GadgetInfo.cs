using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GadgetInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI cost;

    public void SetGadget(Gadget gadget)
    {
        name.text = gadget.Name;
        description.text = gadget.Description;
        cost.text = gadget.Cost.ToString();
    }
}
