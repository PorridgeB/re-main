using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MechanicStoreGadgets : MonoBehaviour
{
    public SaveSO Save;

    [SerializeField]
    private GameObject gadgetRowPrefab;
    [SerializeField]
    private GameObject gadgetList;
    [SerializeField]
    private GadgetInfo gadgetInfo;
    private Gadget selectedGadget;

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

            var isEquipped = gadget.name == Save.SelectedLoadout.Gadget;
            gadgetRow.Equipped = isEquipped;
            gadgetRow.Gadget = gadget;
        }

        if (selectedGadget == null)
        {
            selectedGadget = gadgets.FirstOrDefault();
        }

        gadgetInfo.ShowGadget(selectedGadget, Save);
    }

    public void OnGadgetSelected(Gadget gadget)
    {
        selectedGadget = gadget;

        gadgetInfo.ShowGadget(gadget, Save);
    }
}
