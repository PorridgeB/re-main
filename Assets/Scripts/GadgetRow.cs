using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GadgetRow : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI cost;

    [HideInInspector]
    public Gadget Gadget;
    public bool Equipped = false;

    private void Start()
    {
        name.text = Gadget.Name;
        cost.text = $"{Gadget.Cost} <sprite=1>";

        if (Equipped)
        {
            var buttonImage = GetComponent<Image>();
            buttonImage.color = Color.white;
            name.color = Color.white;
            icon.color = Color.white;
        }
    }

    public void Select()
    {
        SendMessageUpwards("OnGadgetSelected", Gadget);
    }
}
