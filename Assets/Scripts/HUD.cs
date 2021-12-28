using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private FloatReference health;
    [SerializeField]
    private Attribute maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health.Value / maxHealth.Value();
    }
}
