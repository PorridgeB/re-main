using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentInfo : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI cost;

    public void ShowAttachment(WeaponAttachment attachment)
    {
        if (attachment == null)
        {
            name.text = "Name";
            description.text = "Description";
            cost.text = "100 <sprite=1>";

            return;
        }

        name.text = attachment.Name;
        description.text = attachment.Description;
        cost.text = $"{attachment.Cost} <sprite=1>";
    }
}
