using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vitals : MonoBehaviour
{
    [SerializeField]
    private FloatReference playerCurrentHealth;
    [SerializeField]
    private Attribute playerMaxHealth;
    [SerializeField]
    private FloatReference playerCurrentEnergy;
    [SerializeField]
    private Attribute playerMaxEnergy;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private List<Image> energyCells;
    [SerializeField]
    private List<Image> cellFills;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image i in energyCells)
        {
            i.gameObject.SetActive(false);
            cellFills.Add(i.GetComponentsInChildren<Image>()[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerCurrentHealth.Value / playerMaxHealth.Value();
        int index = 0;
        foreach (Image i in energyCells)
        {
            i.gameObject.SetActive(index < playerMaxEnergy.Value());
            cellFills[index].fillAmount = index < playerCurrentEnergy.Value ? 1 : 0;
            
            index++;
        }
    }
}
