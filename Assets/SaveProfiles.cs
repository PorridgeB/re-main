using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveProfiles : MonoBehaviour
{
    public enum Mode
    {
        Continue,
        NewGame,
    }

    public Mode CurrentMode = Mode.Continue;

    [SerializeField]
    private Toggle delete;
    [SerializeField]
    private GameObject profiles;
    [SerializeField]
    private GameObject yesNoDialog;

    private void Start()
    {
        var save = new Save { DataFragments = 42, Scrap = 64, TotalTime = 300, Loadouts = new List<Loadout> { new Loadout { Weapon1 = "A", Weapon2 = "B", Gadget = "C" } } };

        Debug.Log(JsonUtility.ToJson(save));

        foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>().Take(2))
        {
            saveProfile.Save = save;
            saveProfile.Interactable = true;
            saveProfile.Refresh();
        }
    }

    public void Refresh()
    {
        switch (CurrentMode)
        {
            case Mode.Continue:
                foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>())
                {
                    if (saveProfile.Save == null)
                    {
                        saveProfile.Interactable = false;
                    }
                }
                break;
            case Mode.NewGame:
                foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>())
                {
                    saveProfile.Interactable = true;
                }
                break;
        }
    }

    private void OnSaveProfileSelected(SaveProfile saveProfile)
    {
        if (delete.isOn && !saveProfile.IsEmpty)
        {
            var dialog = Instantiate(yesNoDialog, transform).GetComponent<YesNoDialog>();

            dialog.Prompt = "Are you sure you want to delete this save profile?";
            dialog.OnYes += delegate { saveProfile.Delete(); saveProfile.Refresh(); };
        }

        delete.isOn = false;
    }

    public void EnterMode(string mode)
    {
        switch (mode)
        {
            case "Continue":
                break;
            case "NewGame":
                break;
        }
    }
    
    public void EnterContinue()
    {
        CurrentMode = Mode.Continue;

        Refresh();
    }

    public void EnterNewGame()
    {
        CurrentMode = Mode.NewGame;

        Refresh();
    }
}
