using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject statMenu;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private ModuleInventory moduleInventory;
    [SerializeField]
    private ScrollMenu modulesMenu;
    [SerializeField]
    private ScrollMenu statsMenu;


    // Start is called before the first frame update
    void Start()
    {
        GenerateLists();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            statMenu.SetActive(!statMenu.activeInHierarchy);
        }
    }

    public void GenerateLists()
    {
        Debug.Log("generating lists");
        modulesMenu.GenerateModules(moduleInventory);
        statsMenu.GenerateStats(playerStats);
    }
}
