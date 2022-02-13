using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEditor;

public class MechanicStore : MonoBehaviour
{
    [SerializeField]
    private SaveSO save;
    [SerializeField]
    private TextMeshProUGUI scrap;
    [SerializeField]
    private MechanicStoreGadgets gadgets;

    private void Awake()
    {
        gadgets.Save = save;
    }

    private void Start()
    {
        Refresh();
    }

    public void OnGadgetBuy(Gadget gadget)
    {
        save.MakePurchaseWithScrap(gadget.Cost);
        save.AddGadget(gadget.name);

        Refresh();
    }

    public void OnGadgetEquip(Gadget gadget)
    {
        save.SelectedLoadout.Gadget = gadget.name;

        Refresh();
    }

    public void Refresh()
    {
        scrap.text = $"{save.Scrap} <sprite=1 tint>";

        gadgets.Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
