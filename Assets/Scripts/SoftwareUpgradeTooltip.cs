using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoftwareUpgradeTooltip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private GameObject row;

    private void OnEnable()
    {
        transform.SetParent(GameObject.Find("UI").transform);
        transform.position = row.transform.position;
        transform.localPosition = new Vector3(15, transform.localPosition.y, 0);
        description.text = row.GetComponent<SoftwareUpgradeRow>().SoftwareUpgrade.Description;
    }
}
