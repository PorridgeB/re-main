using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField]
    private PlayerInput input;
    private InputAction overlayAction;


    // Start is called before the first frame update
    void Start()
    {
        overlayAction = input.actions["Overlay"];
        GenerateLists();
    }

    // Update is called once per frame
    void Update()
    {
        if (overlayAction.triggered)
        {
            statMenu.SetActive(!statMenu.activeInHierarchy);
            GenerateLists();
        }
    }

    public void GenerateLists()
    {
        modulesMenu.GenerateModules(moduleInventory);
        statsMenu.GenerateStats(playerStats);
    }
}
