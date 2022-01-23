using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechanicStore : MonoBehaviour
{
    public GameObject GadgetRowPrefab;
    public GameObject GadgetList;
    public GadgetInfo GadgetInfo;

    private void Start()
    {
        var gadgets = Resources.LoadAll<Gadget>("Gadgets");

        foreach (var gadget in gadgets)
        {
            var gadgetRow = Instantiate(GadgetRowPrefab, GadgetList.transform).GetComponentInChildren<GadgetRow>();
            
            gadgetRow.Gadget = gadget;
        }
    }

    private void SelectGadget(Gadget gadget)
    {
        GadgetInfo.SetGadget(gadget);
    }
}
