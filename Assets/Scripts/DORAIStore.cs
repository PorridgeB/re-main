using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DORAIStore : MonoBehaviour
{
    [SerializeField]
    private GameObject softwareUpgradeRowPrefab;
    [SerializeField]
    private GameObject softwareUpgradeList;
    [SerializeField]
    private SoftwareUpgradeConfigurationBar configurationBar;

    private void Start()
    {
        var softwareUpgrades = Resources.LoadAll<SoftwareUpgrade>("SoftwareUpgrades");

        softwareUpgrades = softwareUpgrades.OrderBy(x => x.Points).ToArray();

        foreach (var softwareUpgrade in softwareUpgrades)
        {
            var gadgetRow = Instantiate(softwareUpgradeRowPrefab, softwareUpgradeList.transform).GetComponent<SoftwareUpgradeRow>();
            gadgetRow.SoftwareUpgrade = softwareUpgrade;
        }

        configurationBar.Refresh(new Dictionary<SoftwareUpgrade, int> { { softwareUpgrades[0], 2 }, { softwareUpgrades[1], 3 } });
    }
}
