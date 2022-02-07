using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoftwareUpgradeConfigurationBar : MonoBehaviour
{
    public int Capacity = 10;

    [SerializeField]
    private GameObject BarPrefab;

    public void Refresh(Dictionary<SoftwareUpgrade, int> softwareUpgrades)
    {
        //// Remove previous bars
        //foreach (Transform child in transform)
        //{
        //    Destroy(child.gameObject);
        //}

        //foreach (var softwareUpgrade in softwareUpgrades.OrderBy(x => x.Key.Points * x.Value))
        //{
        //    var bar = Instantiate(BarPrefab, transform);

        //    var barImage = bar.GetComponent<Image>();
        //    barImage.color = softwareUpgrade.Key.Color;

        //    var barRectTrans = bar.GetComponent<RectTransform>();
        //    barRectTrans.sizeDelta = new Vector2(30 * softwareUpgrade.Key.Points * softwareUpgrade.Value, 30);
        //}
    }
}
