using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MechanicStoreGadgets : MonoBehaviour
{
    [SerializeField]
    private GameObject gadgetRowPrefab;
    [SerializeField]
    private GameObject gadgetList;
    [SerializeField]
    private GadgetInfo gadgetInfo;

    private void Start()
    {
        var gadgets = Resources.LoadAll<Gadget>("Gadgets");

        gadgets = gadgets.OrderBy(x => x.Cost).ToArray();

        foreach (var gadget in gadgets)
        {
            var gadgetRow = Instantiate(gadgetRowPrefab, gadgetList.transform).GetComponent<GadgetRow>();
            gadgetRow.Gadget = gadget;
        }

        SelectGadget(gadgets.FirstOrDefault());
    }

    public void SelectGadget(Gadget gadget)
    {
        gadgetInfo.SetGadget(gadget);
    }
}
