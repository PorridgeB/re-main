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
    private PlayerInput inputs;
    private InputAction returnAction;


    // Start is called before the first frame update
    void Start()
    {
        inputs = PlayerController.instance.GetComponent<PlayerInput>();
        returnAction = inputs.actions["Return"];

        GenerateLists();
    }

    // Update is called once per frame
    void Update()
    {
        statMenu.SetActive(inputs.currentActionMap.name == "OverlayControl");
        if (statMenu.activeInHierarchy)
        {
            GenerateLists();
        }
        if (returnAction.triggered)
        {
            inputs.SwitchCurrentActionMap("CharacterControl");
        }
    }

    public void GenerateLists()
    {
        modulesMenu.GenerateModules(moduleInventory);
        statsMenu.GenerateStats(playerStats);
    }
}
