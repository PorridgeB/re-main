using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicStoreWeapons : MonoBehaviour
{
    // Buy and/or equipment attachment for weapon

    [SerializeField]
    private GameObject attachmentRowPrefab;
    [SerializeField]
    private GameObject attachmentList;

    private void Start()
    {
        var attachments = Resources.LoadAll<WeaponAttachment>("WeaponAttachments");

        foreach (var attachment in attachments)
        {
            Instantiate(attachmentRowPrefab, attachmentList.transform);

            //var attachmentRow = Instantiate(attachmentRowPrefab, attachmentList.transform).GetComponent<AttachmentRow>();
            //attachmentRow.Attachment = attachment;
        }
    }

    public void ShowAttachments(string type)
    {

    }
}
