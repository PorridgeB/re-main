using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController2 : MonoBehaviour
{
    private PlayerInput inputs;
    private InputAction pause;
    private InputAction overlay;

    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private GameObject doraiStore;
    [SerializeField]
    private GameObject mechanicStore;
    [SerializeField]
    private GameObject dialogueUI;
    [SerializeField]
    private GameObject moduleScreen;

    private bool changeState;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.Find("Player").GetComponent<PlayerInput>();
        pause = inputs.actions["Pause"];
        overlay = inputs.actions["Overlay"];
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIActive())
        {
            if (inputs.currentActionMap.name != "CharacterControl")
            {
                Debug.Log("UI Disabled, actionmap set to charactercontrol");
                inputs.SwitchCurrentActionMap("CharacterControl");
            }
            
        }
        if (pause.triggered)
        {
            Debug.Log("pressed escape");
            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeInHierarchy);
        }
        if (overlay.triggered)
        {
            moduleScreen.SetActive(!moduleScreen.activeInHierarchy);
        }
    }

    public bool UIActive()
    {
        if (pauseMenu.gameObject.activeInHierarchy == true) return true;
        if (doraiStore.activeInHierarchy == true) return true;
        if (mechanicStore.activeInHierarchy == true) return true;
        if (dialogueUI.activeInHierarchy == true) return true;
        if (moduleScreen.activeInHierarchy == true) return true;
        return false;
    }

    public void ActivateDoraiStore()
    {
        doraiStore.SetActive(true);
    }

    public void ActivateMechanicStore()
    {
        mechanicStore.SetActive(true);
    }

    public void ActivateDialogueUI()
    {
        dialogueUI.SetActive(true);
    }
}
