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
        Refresh();
    }

    public void Refresh()
    {
        var gadgets = Resources.LoadAll<Gadget>("Gadgets");

        gadgets = gadgets.OrderBy(x => x.Cost).ToArray();
        
        // Clear gadget list
        foreach (Transform child in gadgetList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var gadget in gadgets)
        {
            var gadgetRow = Instantiate(gadgetRowPrefab, gadgetList.transform).GetComponent<GadgetRow>();

            var isEquipped = gadget.name == SaveManager.Instance.Save.Loadout.Gadget;
            gadgetRow.Equipped = isEquipped;
            gadgetRow.Gadget = gadget;
        }

        gadgetInfo.ShowGadget(gadgets.FirstOrDefault());
    }

    public void OnGadgetSelected(Gadget gadget)
    {
        gadgetInfo.ShowGadget(gadget);
    }
}
