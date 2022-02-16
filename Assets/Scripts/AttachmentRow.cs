using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentRow : MonoBehaviour
{
    public WeaponAttachment WeaponAttachment;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private Toggle equipped;

    private void Start()
    {
        //icon.sprite = WeaponAttachment.Icon;
        name.text = WeaponAttachment.Name;

        equipped.isOn = false;
        equipped.interactable = false;
    }

    public void Select()
    {
        SendMessageUpwards("OnAttachmentSelected", WeaponAttachment);
    }
}
