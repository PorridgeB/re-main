using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject statMenu;
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

    }

    // Update is called once per frame
    void Update()
    {
        statMenu.SetActive(inputs.currentActionMap.name == "OverlayControl");

        if (statMenu.activeInHierarchy)
        {
        }
        if (returnAction.triggered)
        {
            inputs.SwitchCurrentActionMap("CharacterControl");
        }
    }
}
