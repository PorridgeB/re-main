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
        Delete,
    }

    public Mode CurrentMode = Mode.Continue;

    [SerializeField]
    private GameObject profiles;

    private void Start()
    {
        var save = new Save { DataFragments = 42, Scrap = 64, TotalTime = 300, Loadouts = new List<Loadout> { new Loadout { Weapon1 = "A", Weapon2 = "B", Gadget = "C" } } };

        Debug.Log(JsonUtility.ToJson(save));

        foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>().Take(2))
        {
            saveProfile.Save = save;
            saveProfile.Refresh();
        }

        foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>())
        {
            if (saveProfile.Save == null)
            {
                saveProfile.Interactable = false;
            }
        }
    }

    private void OnSaveProfileSelected(SaveProfile saveProfile)
    {
        switch (CurrentMode)
        {
            case Mode.Continue:
                break;
            case Mode.NewGame:
                break;
            case Mode.Delete:
                break;
        }
    }
}
