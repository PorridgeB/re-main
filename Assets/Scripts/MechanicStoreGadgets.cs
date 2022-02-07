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

            gadgetRow.Equipped = gadget.name == SaveManager.Instance.Save.Loadout.Gadget;
            gadgetRow.Gadget = gadget;
        }

        gadgetInfo.ShowGadget(gadgets.FirstOrDefault());
    }

    public void OnGadgetSelected(Gadget gadget)
    {
        gadgetInfo.ShowGadget(gadget);
    }

    public void OnGadgetBuy(Gadget gadget)
    {
        var save = SaveManager.Instance.Save;
        save.Scrap -= gadget.Cost;
        save.UnlockedGadgets.Add(gadget.name);
    }
}