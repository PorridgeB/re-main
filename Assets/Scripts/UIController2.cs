using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController2 : MonoBehaviour
{
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private GameObject doraiStore;
    [SerializeField]
    private GameObject mechanicStore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateDoraiStore()
    {
        doraiStore.SetActive(true);
    }

    public void ActivateMechanicStore()
    {
        mechanicStore.SetActive(true);
    }
}
