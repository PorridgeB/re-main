using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoftwareUpgradeRow : MonoBehaviour
{
    public SoftwareUpgrade SoftwareUpgrade;

    [SerializeField]
    private new TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI counter;

    private void Start()
    {
        name.text = SoftwareUpgrade.Name;
        counter.text = "0";
    }
}
