using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MechanicStoreWeapons : MonoBehaviour
{
    // Buy and/or equipment attachment for weapon

    [SerializeField]
    private GameObject attachmentRowPrefab;
    [SerializeField]
    private GameObject attachmentList;
    [SerializeField]
    private AttachmentInfo attachmentInfo;
    private WeaponAttachment[] attachments;

    private void Start()
    {
        attachments = Resources.LoadAll<WeaponAttachment>("WeaponAttachments");

        ShowAttachments("Phaser");
    }

    public void ClearAttachments()
    {
        foreach (Transform child in attachmentList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowAttachments(string weapon)
    {
        ClearAttachments();

        foreach (var attachment in attachments.Where(x => x.Weapon == weapon))
        {
            var attachmentRow = Instantiate(attachmentRowPrefab, attachmentList.transform).GetComponent<AttachmentRow>();
            
            attachmentRow.WeaponAttachment = attachment;

            //var button = attachmentRow.GetComponent<Button>();
            //button.onClick.AddListener(() => attachmentInfo.ShowAttachment(attachment));
        }

        attachmentInfo.ShowAttachment(attachments.Where(x => x.Weapon == weapon).FirstOrDefault());
    }

    public void OnAttachmentSelected(WeaponAttachment attachment)
    {
        attachmentInfo.ShowAttachment(attachment);
    }
}
