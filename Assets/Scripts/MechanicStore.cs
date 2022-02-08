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
    private TextMeshProUGUI scrap;
    [SerializeField]
    private MechanicStoreGadgets gadgets;

    private void Awake()
    {
        //SaveManager.Instance.Save.Scrap += 190;
    }

    private void Start()
    {
        Refresh();
    }

    public void OnGadgetBuy(Gadget gadget)
    {
        var save = new Save();// SaveManager.Instance.Save;
        save.Scrap -= gadget.Cost;
        save.UnlockedGadgets.Add(gadget.name);

        Refresh();
    }

    public void OnGadgetEquip(Gadget gadget)
    {
        var save = new Save();// SaveManager.Instance.Save;
        //save.Loadout.Gadget = gadget.name;

        Refresh();
    }

    public void Refresh()
    {
        //scrap.text = $"{SaveManager.Instance.Save.Scrap} <sprite=1 tint>";

        gadgets.Refresh();
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
